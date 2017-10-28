using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;

namespace MemoryManager
{
    /// <summary>
    /// Searches memory for a value controlled by the SearchContext
    /// </summary>
    public class Search : ISearchMemory
    {     
        /// <summary>
        /// search thread
        /// </summary>
        private Thread _searchThread;
        private const int MAX_ADDRESS_DISPLAY = 200;

        /// <summary>
        /// Access memory for read, write, and virtual query
        /// </summary>
        private IMemoryAccess _access;

        /// <summary>
        /// Used for updating the calling controller        
        /// </summary>
        private IInvoke _control;

        private const int MAX_BUFFER = 2000000;
        private int _addressesFound = 0;

        private AddressCollection _addressCollection;

        /// <summary>
        /// The current search context
        /// </summary>
        public SearchContext SearchContext
        {
            get
            {
                return _addressCollection.GetSearchContext();
            }
        }        

        /// <summary>
        /// event handlers
        /// </summary>        
        public event EventHandler<AddressFoundEventArgs> OnValueFound; 
        /// <summary>
        /// When the progress of the search changes
        /// </summary>                       
        public event EventHandler<SearchUpdateEventArgs> OnProgressChange;
        /// <summary>
        /// Updates the status text
        /// </summary>
        public event EventHandler<UpdateArgs> OnUpdate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="access"></param>
        /// <param name="control"></param>
        public Search(IMemoryAccess access, IInvoke control)
        {            
            _access = access;
            _control = control;
            _addressCollection = new AddressCollection();           
        }

        /// <summary>
        /// Cancels the search thread.
        /// </summary>
        public void CancelSearch()
        {
            if (_searchThread != null)
            {
                _searchThread.Abort();
                _searchThread.Join();
            }

            //final update
            UpdateProgress(0, 1, 1, 0);
        }

        /// <summary>
        /// Starts a new search thread. Call Cancel to stop it.
        /// </summary>
        /// <param name="context"></param>
        public void NewSearch(SearchContext context)
        {
            _addressCollection.ResetSearch(context);
            CancelSearch();
            
            _searchThread = new Thread(new ParameterizedThreadStart(FirstSearch));  
            _searchThread.Name = "FirstSearchThread";            

            _searchThread.Start(context);
        }

        /// <summary>
        /// Starts the next search thread. This is only called after the new search is complete.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="optValue"></param>
        public void NextSearch(SearchType type, string optValue)
        {
            _addressCollection.StartNextSearch(type, optValue);            

            CancelSearch();

            _searchThread = new Thread(new ParameterizedThreadStart(NextSearch));
            _searchThread.Name = "NextSearchThread";

            _searchThread.Start(_addressCollection.GetSearchContext());
        }

        private void FirstSearch(object obj)
        {
            try
            {
                Update("Starting First Search...");

                SearchContext context = (SearchContext)obj;                
                long start = (long)_access.MinAddress;
                long end = (long)_access.MaxAddress;                                

                // Reset addresses found
                _addressesFound = 0;

                // Search through each read block of memory 
                for (long address = start; address < end; /*increment Address in the inner loop*/ )
                {                    
                    MEMORY_BASIC_INFORMATION info = _access.VirtualQuery(new IntPtr(address));

                    //There was an error with the virtual query
                    //Exit the loop.
                    if ((long)info.RegionSize == 0)                                            
                        break;

                    //Is the memory address for the process I'm looking at?
                    if (info.Protect == AllocationProtectEnum.PAGE_READWRITE && info.State == StateEnum.MEM_COMMIT)
                    {

                        int bufferSize = Math.Min((int)info.RegionSize, MAX_BUFFER);
                        byte[] buffer = new byte[bufferSize];                                            

                        // Read the memory                 
                        int count = _access.ReadMemoryAsBytes(new IntPtr(address), ref buffer);
                        
                        int addressCount = count;

                        if (count == 0)
                        {
                            Update("Nothing to Read " + address);                            
                            address++;
                            continue;
                        }

                        Update("Processing " + addressCount + " addresses...");

                        // If searching for an unkown value just save all memory
                        if (context.SearchType == SearchType.UnKnown)
                        {
                            _addressCollection.AddressWriter.WriteRegion(address, buffer, addressCount);
                            address += addressCount;

                            // Every address is found!
                            _addressesFound += addressCount;
                        }
                        else
                        {
                            SearchBlock(context, address, buffer, addressCount);
                            address += addressCount;
                        }
                    }
                    // This is not a region we care about just skip to the next one.
                    else
                    {
                        Update("skipting " + (long)info.RegionSize);
                        address += (long)info.RegionSize;
                    }
                    
                }                               
                
                UpdateProgress(start, end, end, _addressesFound);
                Update("Done.");
            }
            catch (ThreadAbortException)
            {
                Update("Cancelled.");
                //just exit
            }
            catch (Exception ex)
            {
                Update("Error: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void NextSearch(object obj)
        {
            try
            {
                Update("Searching...");

                SearchContext context = (SearchContext)obj;

                // Reset addresses found
                _addressesFound = 0;                

                AddressReader.Block block = _addressCollection.AddressReader.Next();

                while (block != null)
                {
                    SearchBlock(context, block.baseAddress, block.data, block.data.Length);

                    block = _addressCollection.AddressReader.Next();
                }

                UpdateProgress(0, 100, 100, _addressesFound);
                Update("Done.");

            }
            catch (ThreadAbortException)
            {
                Update("Cancelled.");
                //just exit
            }
            catch (Exception ex)
            {
                Update("Error: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Used to search a memory block.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="address"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        private void SearchBlock(SearchContext context, long address, byte[] buffer, int bufferLength)
        {
            int updateCounter = 0;
            const int MAX_COUNT_UPDATE = 10000;
            bool match = true;  // Do we have a match. If true keep this value.
            double difference;  // Different in before and after values

            byte[] value2compare = context.Value;

            // search through the block. Subtract the size of the data at the end.
            // this will also update three counters. One for the buffer index. One for that address in memory
            // One for the update event firing.
            for (int index = 0; index < bufferLength; index++, address++, updateCounter++)
            {

                // Can't read anymore just exit.
                if (index + context.DataLength > bufferLength)
                    break;

                //update
                if (updateCounter > MAX_COUNT_UPDATE)
                {
                    UpdateProgress(0, bufferLength, index, _addressesFound);
                    updateCounter = 0;
                }                

                if (context.DataType == DataType.Float)
                {
                    float v1 = BitConverter.ToSingle(buffer, index);
                    if (float.IsNaN(v1) || float.IsNegativeInfinity(v1))
                        continue;
                }

                if (context.DataType == DataType.Double)
                {
                    double v1 = BitConverter.ToDouble(buffer, index);
                    if (double.IsNaN(v1) || double.IsNegativeInfinity(v1))
                        continue;
                }
                                                
                // If we are not searching for an excact value we need to compare to the 
                // current value in memory.
                if(context.SearchType != SearchType.FirstExcat)
                    value2compare = _access.ReadMemoryAsBytes(new IntPtr(address), context.DataLength);

                // Check it the values match
                switch (context.SearchType)
                {
                    case SearchType.FirstExcat:
                        match = (Compare(buffer, index, context.Value, context.DataType, out difference) == CompareType.EqualTo);
                        break;
                    case SearchType.Excat:
                        match = (Compare(context.Value, 0, value2compare, context.DataType, out difference) == CompareType.EqualTo);
                        break;
                    case SearchType.HasIncreased:                        
                        match = (Compare(buffer, index, value2compare, context.DataType, out difference) == CompareType.GreaterThen);
                        break;
                    case SearchType.HasDecreased:
                        match = (Compare(buffer, index, value2compare, context.DataType, out difference) == CompareType.LessThen);
                        break;
                    case SearchType.HasNotChanged:
                        match = (Compare(buffer, index, value2compare, context.DataType, out difference) == CompareType.EqualTo);
                        break;
                    case SearchType.HasChanged:
                        match = (Compare(buffer, index, value2compare, context.DataType, out difference) != CompareType.EqualTo);
                        break;
                    case SearchType.HasIncreasedBy:
                        match = (Compare(buffer, index, value2compare, context.DataType, out difference) != CompareType.GreaterThen);
                        switch (context.DataType)
                        {
                            case DataType.UByte:
                            case DataType.UInt16:
                            case DataType.UInt32:
                                match = ((uint)difference == (uint)context.Difference);
                                break;
                            case DataType.Byte:
                            case DataType.Int16:
                            case DataType.Int32:
                                match = ((int)difference == (int)context.Difference);
                                break;
                            case DataType.StringByte:
                            case DataType.StringChar:
                                break;
                            case DataType.Float:
                            case DataType.Double:
                                match = (difference == context.Difference);
                                break;
                        }
                        break;
                    case SearchType.HasDecreasedBy:
                        match = (Compare(buffer, index, value2compare, context.DataType, out difference) != CompareType.LessThen);
                        switch (context.DataType)
                        {
                            case DataType.UByte:
                            case DataType.UInt16:
                            case DataType.UInt32:
                                match = ((uint)difference == (uint)context.Difference);
                                break;
                            case DataType.Byte:
                            case DataType.Int16:
                            case DataType.Int32:
                                match = ((int)difference == (int)context.Difference);
                                break;
                            case DataType.StringByte:
                            case DataType.StringChar:
                                break;
                            case DataType.Float:
                            case DataType.Double:
                                match = (difference == context.Difference);
                                break;
                        }

                        break;
                    default:
                        throw new Exception("Unknown DataType " + context.SearchType);
                }              

                //test if this is want we are looking for
                if (match)
                {
                    // Save this address and its value for the next search
                    _addressCollection.AddressWriter.WriteRegion(address, value2compare, context.DataLength);                   

                    // Let the control know we found something
                    FoundAddress(address, value2compare, context.DataType);
                }               
            }

            //UpdateProgress(0, 100, 100, _addressesFound);
        }       

        private void FoundAddress(long address, byte[] currentValue, DataType data)
        {
            if (_addressesFound < MAX_ADDRESS_DISPLAY)
            {
                //invoke parent
                _control.InvokeMethod(new ThreadStart(delegate ()
                {
                    if (OnValueFound != null)
                    {
                        OnValueFound(this, new AddressFoundEventArgs(new AddressFound(address, currentValue, data)));
                    }
                }));
            }

            // Count this one
            _addressesFound++;
        }          

        private CompareType Compare(byte[] buffer, int bufferIndex, byte[] value2, DataType dataType, out double amoutOfChange)
        {            
            switch (dataType)
            {
                case DataType.Int32:
                    {
                        Int32 old = BitConverter.ToInt32(buffer, bufferIndex);
                        Int32 curr = BitConverter.ToInt32(value2, 0);
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old)
                            return CompareType.LessThen;
                        else if (curr > old)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }                    
                case DataType.Int16:
                    {

                        Int16 old = BitConverter.ToInt16(buffer, bufferIndex);
                        Int16 curr = BitConverter.ToInt16(value2, 0);
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old)
                            return CompareType.LessThen;
                        else if (curr > old)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.Byte:
                    {
                        sbyte old = (sbyte)buffer[bufferIndex];
                        sbyte curr = (sbyte)value2[0];
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old)
                            return CompareType.LessThen;
                        else if (curr > old)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.UInt32:
                    {
                        UInt32 old = BitConverter.ToUInt32(buffer, bufferIndex);
                        UInt32 curr = BitConverter.ToUInt32(value2, 0);
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old)
                            return CompareType.LessThen;
                        else if (curr > old)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.UInt16:
                    {
                        UInt16 old = BitConverter.ToUInt16(buffer, bufferIndex);
                        UInt16 curr = BitConverter.ToUInt16(value2, 0);
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old)
                            return CompareType.LessThen;
                        else if (curr > old)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.UByte:
                    {
                        byte old = buffer[bufferIndex];
                        byte curr = value2[0];
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old)
                            return CompareType.LessThen;
                        else if (curr > old)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.StringChar:
                    {
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        string v1 = encoding.GetString(buffer, bufferIndex, buffer.Length - bufferIndex);
                        string v2 = encoding.GetString(value2);
                        amoutOfChange = 0;
                        if (v1.Equals(v2))
                            return CompareType.EqualTo;
                        else
                            return CompareType.LessThen;
                    }
                case DataType.StringByte:
                    {
                        amoutOfChange = 0;
                        for (int i = bufferIndex; i < buffer.Length; i++)
                        {
                            if (buffer[i] != value2[i])
                                return CompareType.LessThen;
                        }
                        return CompareType.EqualTo;
                    }     
                case DataType.Float:
                    {
                        float old = BitConverter.ToSingle(buffer, bufferIndex);
                        float curr = BitConverter.ToSingle(value2, 0);
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old || float.IsNaN(curr) || float.IsNegativeInfinity(curr))
                            return CompareType.LessThen;
                        else if (curr > old || float.IsPositiveInfinity(curr))
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    } 
                case DataType.Double:
                    {
                        double old = BitConverter.ToDouble(buffer, bufferIndex);
                        double curr = BitConverter.ToDouble(value2, 0);
                        amoutOfChange = Math.Abs(old - curr);
                        if (curr < old || double.IsNaN(curr) || double.IsNegativeInfinity(curr))
                            return CompareType.LessThen;
                        else if (curr > old || double.IsPositiveInfinity(curr))
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    } 
                default:
                    throw new Exception("Unknown DataType");
            }
        }

        private void Update(String detials)
        {
            _control.InvokeMethod(new ThreadStart(delegate ()
            {
                if (OnUpdate != null)
                {
                    OnUpdate(this, new UpdateArgs(detials));
                }
            }));
        }

        private void UpdateProgress(long start, long end, long current, int addressFoundCount)
        {
            long dx = end - start;
            long dy = current - start;
            float precentDone = (float)((float)dy / (float)dx);


            _control.InvokeMethod(new ThreadStart(delegate ()
            {
                if (OnProgressChange != null)
                {
                    OnProgressChange(this, new SearchUpdateEventArgs(start, end, current, addressFoundCount));
                }
            }));
        }

        public void SaveSnapShot(string file)
        {          
            Stream stream = null;
            try
            {
                if(File.Exists(file))
                    File.Delete(file);
                stream  = File.Open(file, FileMode.CreateNew, FileAccess.Write);

                XmlSerializer ser = new XmlSerializer(typeof(AddressCollection));
                ser.Serialize(stream, _addressCollection);
            }
            catch (Exception ex)
            {
                _control.ShowMessage("Error Saving. " + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        public void LoadSnapShot(string file)
        {
            throw new NotImplementedException("LoadSnapShot");
        }
    }
}
