using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MemoryManager.Search
{
    class SearcherImp: ISearcher
    {
        #region Win32 API
        // REQUIRED CONSTS
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;

        // REQUIRED METHODS
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        // REQUIRED STRUCTS
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        #endregion

        /// <summary>
        /// search thread
        /// </summary>
        private Thread _searchThread;      
        private IntPtr _minAddress;
        private IntPtr _maxAddress;
        private ISearchContext _context;
        private ISearchListener _listener;

        public ISearchContext SearchContext
        {
            get
            {
                return _context;
            }
        }

        public void SetSearchListener(ISearchListener listener)
        {
            _listener = listener;
        }

        public SearcherImp()
        {           
            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            _minAddress = sys_info.minimumApplicationAddress;
            _maxAddress = sys_info.maximumApplicationAddress;
        }

        public void Search(ISearchContext context)
        {
            _context = context;

            CancelSearch();

            _searchThread = new Thread(new ParameterizedThreadStart(SearchImp));
            _searchThread.Name = "SerachingThread";

            _searchThread.Start(_context);
        }

        public void SaveContext(string file)
        {
            
        }

        public void LoadContext(string file)
        {
            
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

        private void SearchImp(object obj)
        {
            ISearchContext context = (ISearchContext)obj;
            MemoryAccess access = MemoryAccess.Get;

            if (context.FoundAddresses.Count == 0)
            {
                InitialSearch(context, access);
            }

        }            

        private void InitialSearch(ISearchContext context, MemoryAccess access)
        {
            try
            {               
                IntPtr baseAddress = access.BaseAddress;
                long min = (long)_minAddress;// baseAddress.ToInt32();
                long max = (long)_maxAddress;
                long start = min;
                         
                float lastPrecentDone = 0;

                byte[] target = context.TargetBytes;
                byte[] buffer;                                             
              
                // this will store any information we get from VirtualQueryEx()
                MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();

                int bytesRead = 0;  // number of bytes read with ReadProcessMemory
                
                while (min < max)
                {

                    // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                    VirtualQueryEx(access.ProcessHandle, _minAddress, out mem_basic_info, 28);

                    // if this memory chunk is accessible
                    if (mem_basic_info.Protect == PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
                    {
                        // read everything in the buffer above
                        buffer = access.ReadMemoryAsBytes(mem_basic_info.BaseAddress, mem_basic_info.RegionSize);                                                

                        
                        
                        lastPrecentDone = UpdateProgress(start, max, lastPrecentDone, min, context.FoundAddresses.Count);
                    }
                    min += mem_basic_info.RegionSize;
                    break;
                }

                //final update
                //FoundAddress(context, true);
                UpdateProgress(start, max, lastPrecentDone, max, context.FoundAddresses.Count);
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

        private float UpdateProgress(long start, long end, float lastPrecentDone, long currentAddress, long addressFoundCount)
        {
            float dx = end - start;
            float dy = currentAddress - start;
            float precentDone = (float)(dy / dx);

            //this must change by at least one precent or equal 100%
            if (precentDone - lastPrecentDone > .1f || end == currentAddress)
            {                
                //_listener.InvokeMethod(new ThreadStart(delegate()
                //{
                //   _listener.OnProgressChange( new SearchUpdateEventArgs((int)start, (int)end, (int)currentAddress, (int)addressFoundCount));                    
                //}));
                lastPrecentDone = precentDone;
            }
            return lastPrecentDone;
        }

        public void SaveSnapShot(string file)
        {   
            /*       
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
            */
        }

        public void LoadSnapShot(string file)
        {
            /*
            Stream stream = null;
            try
            {
                stream = File.Open(file, FileMode.Open, FileAccess.Read);

                XmlSerializer ser = new XmlSerializer(typeof(AddressCollection));
                _addressCollection = (AddressCollection)ser.Deserialize(stream);

                //display the first few addresses
                _control.InvokeMethod(new ThreadStart(delegate ()
                {
                    //show how many address are found
                    _control.OnProgressChange(new SearchUpdateEventArgs(0, 1, 0,
                                _addressCollection.CurrentList.Count));

                    //display the values found
                    int count = Math.Min(_addressCollection.CurrentList.Count, AddressDisplayCount);
                    for (int x = 0; x < count; x++)
                    {
                        _control.OnValueFound(new AddressFoundEventArgs(_addressCollection.CurrentList[x]));
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
            */
        }
    }  
}
