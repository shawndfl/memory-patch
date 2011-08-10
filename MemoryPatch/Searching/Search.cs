using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace MemoryPatch
{
    public class SearchMemory
    {     
        /// <summary>
        /// search thread
        /// </summary>
        private Thread _searchThread;
        public int AddressDisplayCount = 400;

        private MemoryAccess _access;
        private Control _parent;

        /// <summary>
        /// snap shot of searches
        /// </summary>
        private List<AddressFound> _snapShot1;
        private List<AddressFound> _snapShot2;         
        private bool _snapShotToggle = true;
        private AddressCollection _addressCollection;
        public SearchContext SearchContext
        {
            get
            {
                return _addressCollection.SearchContext;
            }
        }
        //private SearchContext _context;

        /// <summary>
        /// event handlers
        /// </summary>        
        public event EventHandler<AddressFoundEventArgs> OnValueFound;        
        public event EventHandler<SearchUpdateEventArgs> OnProgressChange;

        public SearchMemory(MemoryAccess access, Control parent)
        {            
            _access = access;
            _parent = parent;
            _addressCollection = new AddressCollection();
            _snapShot1 = new List<AddressFound>(25000);
            _snapShot2 = new List<AddressFound>(25000);
        }

        public void CancelSearch()
        {
            if (_searchThread != null)
            {
                _searchThread.Abort();
                _searchThread.Join();
            }

            //final update
            UpdateProgress(0, 1, 1, 1, 0);
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
                SearchContext data = (SearchContext)obj;

                IntPtr baseAddress = _access.Module.BaseAddress;
                int start = baseAddress.ToInt32();
                int len = _access.Module.ModuleMemorySize;
                int end = start + len;               
                float lastPrecentDone = 0;

                byte[] value1 = data.Value;
                byte[] buffer;

                //reset how the next search will work
                _addressCollection.ResetSearch(data);
                _snapShot1.Clear();
                _snapShotToggle = true;

                for (int Address = start; Address < end; Address++)
                {                    
                    buffer = _access.ReadMemoryAsBytes(Address, data.DataLength);
                    
                    if (data.DataType == DataType.Float)
                    {
                        float v1 = BitConverter.ToSingle(buffer, 0);
                        if (float.IsNaN(v1) || float.IsNegativeInfinity(v1))
                            continue;
                    }

                    if (data.DataType == DataType.Double)
                    {
                        double v1 = BitConverter.ToDouble(buffer, 0);
                        if (double.IsNaN(v1) || double.IsNegativeInfinity(v1))
                            continue;
                    }

                    //check it the values match
                    bool match = true;
                    double difference;
                    switch (data.SearchType)
                    {
                        case SearchType.Excat:
                            match = (Compare(buffer, value1, data.DataType, out difference) == CompareType.EqualTo);
                            break;
                        case SearchType.UnKnown:
                            match = true;
                            break;
                        default:
                            throw new Exception("Unknown DataType " + data.SearchType);
                    }

                    //test if this is want we are looking for
                    if (match)
                    {
                        AddressFound addressFound = new AddressFound(Address, buffer,
                                                             data.DataType);

                        _addressCollection.CurrentList.Add(addressFound); 
                      
                        //FoundAddress(data, _snapShot1, false);                        
                        FoundAddress(data,  false);                        

                        //_snapShot1.Add(addressFound);
                        //int count = _snapShot1.Count;

                        //if (count < 200)
                        //{
                        //    _parent.Invoke(new ThreadStart(delegate()
                        //        {
                        //            if (OnValueFound != null)
                        //            {
                        //                OnValueFound(this, new AddressFoundEventArgs(addressFound));
                        //            }
                        //        }));
                        //}
                    }

                    lastPrecentDone = UpdateProgress(start, end, lastPrecentDone, Address, _addressCollection.CurrentList.Count);
                }

                //final update
                FoundAddress(data, true);
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
                SearchContext data = (SearchContext)obj;

                IntPtr baseAddress = _access.Module.BaseAddress;
                int start = baseAddress.ToInt32();
                int len = _access.Module.ModuleMemorySize;
                int end = start + len;                
                float lastPrecentDone = 0;

                byte[] value1 = data.Value;                
                byte[] buffer;
                
                //this handles consective searches
                List<AddressFound> srcList = _addressCollection.CurrentList;
                List<AddressFound> destList = _addressCollection.LastList;
                _addressCollection.StartNextSearch();
                //if (_snapShotToggle)
                //{
                //    srcList = _snapShot1;
                //    destList = _snapShot2;
                //}
                //else
                //{
                //    srcList = _snapShot2;
                //    destList = _snapShot1;
                //}

                //destList.Clear();
                //_snapShotToggle = !_snapShotToggle;
                
                for (int i = 0; i < srcList.Count; i++)
                {
                    AddressFound currentAddress = srcList[i];
                    buffer = _access.ReadMemoryAsBytes(currentAddress.Address, data.DataLength);                    

                    if (data.DataType == DataType.Float)
                    {
                        float v1 = BitConverter.ToSingle(buffer, 0);
                        if (float.IsNaN(v1) || float.IsNegativeInfinity(v1))
                        {
                            srcList.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }

                    if (data.DataType == DataType.Double)
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
                        switch (data.SearchType)
                        {
                            case SearchType.Excat:
                                match = (Compare(buffer, value1, data.DataType, out diff) == CompareType.EqualTo);
                                break;
                            case SearchType.HasIncreased:
                                match = (Compare(buffer, currentAddress.CurrentValue, data.DataType, out diff) == CompareType.GreaterThen);
                                break;
                            case SearchType.HasDecreased:
                                match = (Compare(buffer, currentAddress.CurrentValue, data.DataType, out diff) == CompareType.LessThen);
                                break;
                            case SearchType.HasNotChanged:
                                match = (Compare(buffer, currentAddress.CurrentValue, data.DataType, out diff) == CompareType.EqualTo);
                                break;
                            case SearchType.HasChanged:
                                match = (Compare(buffer, currentAddress.CurrentValue, data.DataType, out diff) != CompareType.EqualTo);
                                break;
                            case SearchType.HasIncreasedBy:
                                match = (Compare(buffer, currentAddress.CurrentValue, data.DataType, out diff) != CompareType.GreaterThen);
                                switch (data.DataType)
                                {
                                    case DataType.UByte:
                                    case DataType.UInt16:
                                    case DataType.UInt32:
                                        match = ((uint)diff == (uint)data.Difference);
                                        break;
                                    case DataType.Byte:
                                    case DataType.Int16:
                                    case DataType.Int32:
                                        match = ((int)diff == (int)data.Difference);
                                        break;
                                    case DataType.String:
                                        break;
                                    case DataType.Float:
                                    case DataType.Double:
                                        match = (diff == data.Difference);
                                        break;
                                }
                                break;
                            case SearchType.HasDecreasedBy:
                                match = (Compare(buffer, currentAddress.CurrentValue, data.DataType, out diff) != CompareType.LessThen);
                                switch (data.DataType)
                                {
                                    case DataType.UByte:                                        
                                    case DataType.UInt16:                                        
                                    case DataType.UInt32:
                                        match = ((uint)diff == (uint)data.Difference);
                                        break;
                                    case DataType.Byte:                                        
                                    case DataType.Int16:                                        
                                    case DataType.Int32:
                                        match = ((int)diff == (int)data.Difference);
                                        break;
                                    case DataType.String:
                                        break;
                                    case DataType.Float:                                
                                    case DataType.Double:
                                        match = (diff == data.Difference);
                                        break;                                 
                                }
                                
                                break;                            
                            default:
                                throw new Exception("Unknown DataType " + data.SearchType);
                        }

                        //test if this is want we are looking for
                        if (match)
                        {
                            AddressFound foundAddress = new AddressFound(currentAddress.Address, buffer,
                                                 data.DataType);

                            destList.Add(foundAddress);

                            FoundAddress(data, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    lastPrecentDone = UpdateProgress(start, end, lastPrecentDone, 
                        currentAddress.Address, destList.Count);
                }
                            
                //final update
                FoundAddress(data,  true);
                UpdateProgress(start, end, lastPrecentDone, end, destList.Count);
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
                _parent.Invoke(new ThreadStart(delegate()
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
                    _parent.Invoke(new ThreadStart(delegate()
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

        public CompareType Compare(byte[] value1, byte[] value2, DataType dataType, out double amoutOfChange)
        {            
            switch (dataType)
            {
                case DataType.Int32:
                    {
                        Int32 v1 = BitConverter.ToInt32(value1, 0);
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

                        Int16 v1 = BitConverter.ToInt16(value1, 0);
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
                        sbyte v1 = (sbyte)value1[0];
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
                        UInt32 v1 = BitConverter.ToUInt32(value1, 0);
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
                        UInt16 v1 = BitConverter.ToUInt16(value1, 0);
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
                        byte v1 = value1[0];
                        byte v2 = value2[0];
                        amoutOfChange = Math.Abs(v1 - v2);
                        if (v1 < v2)
                            return CompareType.LessThen;
                        else if (v1 > v2)
                            return CompareType.GreaterThen;
                        else
                            return CompareType.EqualTo;
                    }     
                case DataType.String:
                    {
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        string v1 = encoding.GetString(value1);
                        string v2 = encoding.GetString(value2);
                        amoutOfChange = 0;
                        if (v1.Equals(v2))
                            return CompareType.EqualTo;
                        else
                            return CompareType.LessThen;
                    }     
                case DataType.Float:
                    {
                        float v1 = BitConverter.ToSingle(value1, 0);
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
                        double v1 = BitConverter.ToDouble(value1, 0);
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

        private int DataTypeByteCount(DataType dataType)
        {            
            switch (dataType)
            {
                case DataType.Int32:
                   return sizeof(Int32);                   
                case DataType.Int16:
                   return sizeof(Int16);
                case DataType.Byte:
                   return sizeof(sbyte);
                case DataType.UInt32:
                   return sizeof(UInt32);
                case DataType.UInt16:
                   return sizeof(UInt16);
                case DataType.UByte:
                   return sizeof(byte);
                case DataType.String:
                    throw new NotImplementedException();                    
                case DataType.Float:
                    return sizeof(float);
                case DataType.Double:
                    return sizeof(double);
                default:
                    throw new Exception("Unknown DataType");
            }          
        }

        private float UpdateProgress(int start, int end, float lastPrecentDone, int currentAddress, int addressFoundCount)
        {
            float dx = end - start;
            float dy = currentAddress - start;
            float precentDone = (float)(dy / dx);

            //this must change by at least one precent or equal 100%
            if (precentDone - lastPrecentDone > .1f || end == currentAddress)
            {
                _parent.Invoke(new ThreadStart(delegate()
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
                MessageBox.Show("Error Saving. " + ex.Message);
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
                _parent.Invoke(new ThreadStart(delegate()                
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
                MessageBox.Show("Error Opening file " + file + "\n" + ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }

    public class AddressCollection
    {
        public const int AddressCount = 25000;
        private List<AddressFound> _snapShot1 = new List<AddressFound>(AddressCount);
        private List<AddressFound> _snapShot2 = new List<AddressFound>(AddressCount);

        public SearchContext SearchContext;      
        
        public List<AddressFound> CurrentList
        {
            get { return SnapShotToggle ? _snapShot1 : _snapShot2; }           
        }
       
        public List<AddressFound> LastList
        {
            get { return SnapShotToggle ? _snapShot2 : _snapShot1; }
            set { _snapShot1 = value; }
        }


        [XmlIgnore]
        public bool SnapShotToggle = true;

        public void ResetSearch(SearchContext searchContext)
        {
            SearchContext = searchContext;
            _snapShot1.Clear();
            _snapShot2.Clear();
            SnapShotToggle = true;
        }

        public void StartNextSearch()
        {
            SnapShotToggle = !SnapShotToggle;
            CurrentList.Clear();            
        }
    }
}
