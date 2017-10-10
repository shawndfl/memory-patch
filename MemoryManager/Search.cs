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
    public class Search : ISearchMemory
    {     
        /// <summary>
        /// search thread
        /// </summary>
        private Thread _searchThread;
        private  int AddressDisplayCount = 200;

        private IMemoryAccess _access;
        private IInvoke _control;

        private const int MAX_BUFFER = 0xFFFF;


        private AddressCollection _addressCollection;
        public SearchContext SearchContext
        {
            get
            {
                return _addressCollection.SearchContext;
            }
        }        

        /// <summary>
        /// event handlers
        /// </summary>        
        public event EventHandler<AddressFoundEventArgs> OnValueFound;        
        public event EventHandler<SearchUpdateEventArgs> OnProgressChange;

        public Search(IMemoryAccess access, IInvoke control)
        {            
            _access = access;
            _control = control;
            _addressCollection = new AddressCollection();           
        }

        public void CancelSearch()
        {
            if (_searchThread != null)
            {
                _searchThread.Abort();
                _searchThread.Join();
            }

            //final update
            UpdateProgress(0, 1, 1.0f, 1, 0);
        }

        public void NewSearch(SearchContext context)
        {
            _addressCollection.SearchContext = context;
            CancelSearch();
            
            _searchThread = new Thread(new ParameterizedThreadStart(FirstSearch));  
            _searchThread.Name = "SerachingThread";            

            _searchThread.Start(context);
        }

        public void NextSearch(SearchType type, string optValue)
        {
            _addressCollection.SearchContext.SearchType = type;
            _addressCollection.SearchContext.SetValue(optValue);

            CancelSearch();

            _searchThread = new Thread(new ParameterizedThreadStart(NextSearch));
            _searchThread.Name = "SerachingThread";

            _searchThread.Start(_addressCollection.SearchContext);
        }

        private void FirstSearch(object obj)
        {
            try
            {
                SearchContext context = (SearchContext)obj;
                
                long start = (long)_access.MinAddress;
                long end = (long)_access.MaxAddress;                
                float lastPrecentDone = 0;

                byte[] value2 = context.Value;                              
                
                // search through each read block of memory 
                for (long Address = start; Address < end; /*increment Address in the inner loop*/ )
                {                   
                    MEMORY_BASIC_INFORMATION info = _access.VirtualQuery(new IntPtr(Address));

                    //There was an error with the virtual query
                    //go to the next address
                    if ((long)info.RegionSize == 0)
                    {
                        Address++;
                        continue;
                    }
                   
                    //Is the memory address for the process I'm looking at?
                    if (info.Protect == AllocationProtectEnum.PAGE_READWRITE && info.State == StateEnum.MEM_COMMIT)
                    {
                        int bufferSize = Math.Min((int)info.RegionSize, MAX_BUFFER);
                        byte[] buffer = new byte[bufferSize];
                        //Console.WriteLine("Creating buffer: " + (int)info.RegionSize);

                        // read the memory                 
                        int count = _access.ReadMemoryAsBytes(new IntPtr(Address), ref buffer);

                        // we are only going to step what we read - the context data length
                        int adressStep = count - context.DataLength;

                        if (count == 0)
                        {
                            //Console.WriteLine("Can't read from " + (long)Address);
                            Address++;
                            continue;
                        }

                        // search through the block. Subtract the size of the data at the end.
                        for (int index = 0; index < adressStep; index++)
                        {
                            lastPrecentDone = UpdateProgress(start, end, lastPrecentDone, Address,
                               _addressCollection.CurrentList.Count);

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

                            //check it the values match
                            bool match = true;
                            double difference;
                            switch (context.SearchType)
                            {
                                case SearchType.Excat:
                                    match = (Compare(buffer, index, value2, context.DataType, out difference) == CompareType.EqualTo);
                                    break;
                                case SearchType.UnKnown:
                                    match = true;
                                    break;
                                default:
                                    throw new Exception("Unknown DataType " + context.SearchType);
                            }

                            //test if this is want we are looking for
                            if (match)
                            {
                                AddressFound addressFound = new AddressFound(Address, buffer,
                                                                     context.DataType);

                                _addressCollection.CurrentList.Add(addressFound);

                                FoundAddress(context, false);
                            }

                            Address++;
                        }                        
                    }
                    // This is not a region we care about just skip to the next one.
                    else
                    {
                        Address += (long)info.RegionSize;
                    }
                    
                }

                //final update
                FoundAddress(context, true);
                UpdateProgress(start, end, lastPrecentDone, end, _addressCollection.CurrentList.Count);
            }
            catch (ThreadAbortException)
            {
                //just exit
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void NextSearch(object obj)
        {
            try
            {
                SearchContext context = (SearchContext)obj;
                              
                float lastPrecentDone = 0;

                byte[] value1 = context.Value;
                byte[] buffer;

                //this handles consective searches
                List<AddressFound> srcList = _addressCollection.CurrentList;
                List<AddressFound> destList = _addressCollection.LastList;
                _addressCollection.StartNextSearch();            

                for (int i = 0; i < srcList.Count; i++)
                {
                    AddressFound currentAddress = srcList[i];
                    buffer = _access.ReadMemoryAsBytes(new IntPtr(currentAddress.Address), context.DataLength);

                    lastPrecentDone = UpdateProgress(0, srcList.Count, lastPrecentDone,
                       i, destList.Count);
               

                    if (context.DataType == DataType.Float)
                    {
                        float v1 = BitConverter.ToSingle(buffer, 0);
                        if (float.IsNaN(v1) || float.IsNegativeInfinity(v1))
                        {
                            srcList.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

                    if (context.DataType == DataType.Double)
                    {
                        double v1 = BitConverter.ToDouble(buffer, 0);
                        if (double.IsNaN(v1) || double.IsNegativeInfinity(v1))
                        {
                            srcList.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

                    try
                    {
                        double diff;
                        bool match = true;
                        switch (context.SearchType)
                        {                            
                            case SearchType.CompareToSnapShot1:
                            case SearchType.CompareToSnapShot2:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) == CompareType.EqualTo);
                                break;
                            case SearchType.Excat:
                                match = (Compare(buffer, value1, context.DataType, out diff) == CompareType.EqualTo);
                                break;
                            case SearchType.HasIncreased:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) == CompareType.GreaterThen);
                                break;
                            case SearchType.HasDecreased:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) == CompareType.LessThen);
                                break;
                            case SearchType.HasNotChanged:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) == CompareType.EqualTo);
                                break;
                            case SearchType.HasChanged:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) != CompareType.EqualTo);
                                break;
                            case SearchType.HasIncreasedBy:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) != CompareType.GreaterThen);
                                switch (context.DataType)
                                {
                                    case DataType.UByte:
                                    case DataType.UInt16:
                                    case DataType.UInt32:
                                        match = ((uint)diff == (uint)context.Difference);
                                        break;
                                    case DataType.Byte:
                                    case DataType.Int16:
                                    case DataType.Int32:
                                        match = ((int)diff == (int)context.Difference);
                                        break;
                                    case DataType.StringByte:
                                    case DataType.StringChar:
                                        break;
                                    case DataType.Float:
                                    case DataType.Double:
                                        match = (diff == context.Difference);
                                        break;
                                }
                                break;
                            case SearchType.HasDecreasedBy:
                                match = (Compare(buffer, currentAddress.CurrentValue, context.DataType, out diff) != CompareType.LessThen);
                                switch (context.DataType)
                                {
                                    case DataType.UByte:
                                    case DataType.UInt16:
                                    case DataType.UInt32:
                                        match = ((uint)diff == (uint)context.Difference);
                                        break;
                                    case DataType.Byte:
                                    case DataType.Int16:
                                    case DataType.Int32:
                                        match = ((int)diff == (int)context.Difference);
                                        break;
                                    case DataType.StringByte:
                                    case DataType.StringChar:
                                        break;
                                    case DataType.Float:
                                    case DataType.Double:
                                        match = (diff == context.Difference);
                                        break;
                                }

                                break;                         
                            default:
                                throw new Exception("Unknown DataType " + context.SearchType);
                        }

                        //test if this is want we are looking for
                        if (match)
                        {
                            AddressFound foundAddress = new AddressFound(currentAddress.Address, buffer,
                                                 context.DataType);

                            destList.Add(foundAddress);

                            FoundAddress(context, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }                   
                }

                //final update
                FoundAddress(context, true);
                UpdateProgress(0, srcList.Count, lastPrecentDone, srcList.Count, destList.Count);
            }
            catch (ThreadAbortException)
            {
                //just exit
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        //private void FoundAddress(SearchContext data, 
        //    List<AddressFound> destList, bool showRest)
        private void FoundAddress(SearchContext data, bool showRest)
        {            
            int count = _addressCollection.CurrentList.Count;
            int offten = 5;
            int rest = count % offten;
            List<AddressFound> destList = _addressCollection.CurrentList;

            if (showRest && rest != 0)
            {
                
                //copy addresses to array
                int index = 0;
                AddressFound[] addresses = new AddressFound[offten];
                for (int x = count - rest; x < count; x++)
                {
                    addresses[index++] = destList[x];
                }

                //invoke parent
                _control.InvokeMethod(new ThreadStart(delegate()
                {
                    if (OnValueFound != null)
                    {
                        for (int x = 0; x < rest; x++)
                        {
                            OnValueFound(this, new AddressFoundEventArgs(addresses[x]));
                        }
                    }
                }));
            }
            else if(!showRest)
            {
                //only show the first AddressDisplayCount and show every 5th address
                if (count < AddressDisplayCount &&
                    count % offten == 0 &&
                    count - offten >= 0)
                {
                    //copy addresses to array
                    int index = 0;
                    AddressFound[] addresses = new AddressFound[offten];
                    for (int x = count - offten; x < count; x++)
                    {
                        addresses[index++] = destList[x];
                    }

                    //invoke parent
                    _control.InvokeMethod(new ThreadStart(delegate()
                    {
                        if (OnValueFound != null)
                        {
                            for (int x = 0; x < offten; x++)
                            {
                                OnValueFound(this, new AddressFoundEventArgs(addresses[x]));
                            }
                        }
                    }));
                }
            }
        }

        private CompareType Compare(byte[] buffer, byte[] value2, DataType dataType, out double amoutOfChange)
        {
            return Compare(buffer, 0, value2, dataType, out amoutOfChange);
        }

        private CompareType Compare(byte[] buffer, int bufferIndex, byte[] value2, DataType dataType, out double amoutOfChange)
        {            
            switch (dataType)
            {
                case DataType.Int32:
                    {
                        Int32 v1 = BitConverter.ToInt32(buffer, bufferIndex);
                        Int32 v2 = BitConverter.ToInt32(value2, 0);
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }                    
                case DataType.Int16:
                    {

                        Int16 v1 = BitConverter.ToInt16(buffer, bufferIndex);
                        Int16 v2 = BitConverter.ToInt16(value2, 0);
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.Byte:
                    {
                        sbyte v1 = (sbyte)buffer[bufferIndex];
                        sbyte v2 = (sbyte)value2[0];
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.UInt32:
                    {
                        UInt32 v1 = BitConverter.ToUInt32(buffer, bufferIndex);
                        UInt32 v2 = BitConverter.ToUInt32(value2, 0);
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.UInt16:
                    {
                        UInt16 v1 = BitConverter.ToUInt16(buffer, bufferIndex);
                        UInt16 v2 = BitConverter.ToUInt16(value2, 0);
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.UByte:
                    {
                        byte v1 = buffer[bufferIndex];
                        byte v2 = value2[0];
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
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
                        float v1 = BitConverter.ToSingle(buffer, bufferIndex);
                        float v2 = BitConverter.ToSingle(value2, 0);
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2 || float.IsNaN(v1) || float.IsNegativeInfinity(v1))
                            return CompareType.LessThen;
                        else if (v1 > v2 || float.IsPositiveInfinity(v1))
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    } 
                case DataType.Double:
                    {
                        double v1 = BitConverter.ToDouble(buffer, bufferIndex);
                        double v2 = BitConverter.ToDouble(value2, 0);
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2 || double.IsNaN(v1) || double.IsNegativeInfinity(v1))
                            return CompareType.LessThen;
                        else if (v1 > v2 || double.IsPositiveInfinity(v1))
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    } 
                default:
                    throw new Exception("Unknown DataType");
            }
        }                

        private float UpdateProgress(long start, long end, float lastPrecentDone, long currentAddress, int addressFoundCount)
        {
            long dx = end - start;
            long dy = currentAddress - start;
            float precentDone = (float)((float)dy / (float)dx);

            //this must change by at least one precent or equal 100%
            if (precentDone - lastPrecentDone > .1f || end == currentAddress)
            {              
                _control.InvokeMethod(new ThreadStart(delegate()
                {
                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new SearchUpdateEventArgs(start, end, currentAddress, addressFoundCount));
                    }
                }));
                lastPrecentDone = precentDone;
            }
            return lastPrecentDone;
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
            Stream stream = null;
            try
            {
                stream = File.Open(file, FileMode.Open, FileAccess.Read);

                XmlSerializer ser = new XmlSerializer(typeof(AddressCollection));
                _addressCollection = (AddressCollection)ser.Deserialize(stream);                

                //display the first few addresses
                _control.InvokeMethod(new ThreadStart(delegate()                
                {
                    //show how many address are found
                    if (OnProgressChange != null)
                    {
                        OnProgressChange(this, new SearchUpdateEventArgs(0, 1, 0,
                            _addressCollection.CurrentList.Count));
                    }

                    //display the values found
                    if (OnValueFound != null)
                    {
                        int count = Math.Min(_addressCollection.CurrentList.Count, AddressDisplayCount);
                        for (int x = 0; x < count; x++)
                        {
                            OnValueFound(this, new AddressFoundEventArgs(_addressCollection.CurrentList[x]));
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                _control.ShowMessage("Error Opening file " + file + "\n" + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}
