using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MemoryManager
{
    //WORD = ushort
    //DWORD = uint
    //long = int
    //BYTE = byte
    //ULONGLONG = ulong

    /// <summary>
    /// A portable exe class
    /// </summary>
    public class PortableExe : IDisposable
    {        
        #region Const
        public const ushort IMAGE_DOS_SIGNATURE = 0x5A4D;  // MZ
        public const ushort IMAGE_OS2_SIGNATURE = 0x454E;   // NE
        public const ushort IMAGE_OS2_SIGNATURE_LE = 0x454C;    // LE
        public const ushort IMAGE_VXD_SIGNATURE = 0x454C;     // LE
        public const ushort IMAGE_NT_SIGNATURE = 0x00004550;  // PE00
        public const ushort SIZE_OF_NT_SIGNATURE = 0x04; //size of c++ long
        #endregion        

        #region Fields        
        private BinaryReader _reader;
        private long _initialOffset;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the dos header.
        /// </summary>
        private _IMAGE_DOS_HEADER DosHeader { get; set; }

        private Stream Stream
        {
            get { return _reader.BaseStream; } 
        }
        /// <summary>
        /// The name of the file
        /// </summary>
        public string FileName { get; private set; }
        #endregion        

        /// <summary>
        /// Opens the file
        /// </summary>
        /// <param name="file"></param>
        public void OpenFile(string file, bool allowEdits)
        {
            Dispose();
            
            FileName = file;
            _initialOffset = 0;

            Stream stream;
            if (allowEdits)            
                stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);            
            else            
                stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        
            _reader = new BinaryReader(stream);
        }

        /// <summary>
        /// Reads some stuff
        /// </summary>
        /// <returns></returns>
        public void Read()
        {          
            Stream.Position = 0;
            DosHeader = new _IMAGE_DOS_HEADER(_reader);

            if (DosHeader.e_magic == IMAGE_DOS_SIGNATURE)
            {
                _reader.BaseStream.Position = DosHeader.e_lfanew + _initialOffset;
                uint signature = ReadInt(_reader);

                if (signature == IMAGE_NT_SIGNATURE)
                {
                    _IMAGE_FILE_HEADER fileHeader = new _IMAGE_FILE_HEADER(_reader);
                    _IMAGE_OPTIONAL_HEADER optHeader = new _IMAGE_OPTIONAL_HEADER(_reader);
                }
                
            }            
        }

        internal static ushort ReadShort(BinaryReader reader)
        {
            byte[] b = reader.ReadBytes(2);
            return ByteConverter.ToUInt16(b, 0);
        }

        internal static uint ReadInt(BinaryReader reader)
        {
            byte[] b = reader.ReadBytes(4);
            return ByteConverter.ToUInt32(b, 0);
        }      

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_reader != null)
                _reader.Close();
        }

        #endregion
    }

    class _IMAGE_DOS_HEADER
    {
        
        #region Fields
        private long _streamInitialOffset;        
        #endregion

        #region Properties
        // DOS .EXE header
        public ushort e_magic { get; private set; }         // Magic number
        public ushort e_cblp { get; private set; }         // Bytes on last page of file
        public ushort e_cp { get; private set; }           // Pages in file
        public ushort e_crlc { get; private set; }         // Relocations
        public ushort e_cparhdr { get; private set; }      // Size of header in paragraphs
        public ushort e_minalloc { get; private set; }     // Minimum extra paragraphs needed
        public ushort e_maxalloc { get; private set; }     // Maximum extra paragraphs needed
        public ushort e_ss { get; private set; }           // Initial (relative) SS value
        public ushort e_sp { get; private set; }           // Initial SP value
        public ushort e_csum { get; private set; }         // Checksum
        public ushort e_ip { get; private set; }           // Initial IP value
        public ushort e_cs { get; private set; }           // Initial (relative) CS value
        public ushort e_lfarlc { get; private set; }       // File address of relocation table
        public ushort e_ovno { get; private set; }         // Overlay number
        public ushort[] e_res { get; private set; }       // Reserved words 4
        public ushort e_oemid { get; private set; }        // OEM identifier (for e_oeminfo)
        public ushort e_oeminfo { get; private set; }      // OEM information; e_oemid specific
        public ushort[] e_res2 { get; private set; }     // Reserved words 10
        public int e_lfanew { get; private set; }       // File address of new exe header       
        #endregion

        public _IMAGE_DOS_HEADER(BinaryReader reader)
        {                                 
            //read in header            
            e_magic = reader.ReadUInt16();

            e_cblp = reader.ReadUInt16();
            e_cp = reader.ReadUInt16();
            e_crlc = reader.ReadUInt16();
            e_cparhdr = reader.ReadUInt16();
            e_minalloc = reader.ReadUInt16();
            e_maxalloc = reader.ReadUInt16();
            e_ss = reader.ReadUInt16();
            e_sp = reader.ReadUInt16();
            e_csum = reader.ReadUInt16();
            e_ip = reader.ReadUInt16();
            e_cs = reader.ReadUInt16();
            e_lfarlc = reader.ReadUInt16();
            e_ovno = reader.ReadUInt16();

            e_res = new ushort[4];
            for (int i = 0; i < 4; i++)
                e_res[i] = reader.ReadUInt16();

            e_oemid = reader.ReadUInt16();
            e_oeminfo = reader.ReadUInt16();

            e_res2 = new ushort[10];
            for (int i = 0; i < 10; i++)
                e_res2[i] = reader.ReadUInt16();

            e_lfanew = reader.ReadInt32();            
        }      
    }

    class _IMAGE_FILE_HEADER
    {
        private ushort _machine;

        public MachineName MachineName { get; private set; }
        public IMAGE_FILE_Characteristics Characteristics { get; private set; }
        
        public ushort NumberOfSections { get; private set; }
        public uint TimeDateStamp { get; private set; }
        public uint PointerToSymbolTable { get; private set; }
        public uint NumberOfSymbols { get; private set; }
        public ushort SizeOfOptionalHeader { get; private set; }
        public ushort _characteristics;

        public _IMAGE_FILE_HEADER(BinaryReader reader)
        {
            _machine = reader.ReadUInt16();
            NumberOfSections = reader.ReadUInt16();
            TimeDateStamp = reader.ReadUInt32();
            PointerToSymbolTable = reader.ReadUInt32();
            NumberOfSymbols = reader.ReadUInt32();
            SizeOfOptionalHeader = reader.ReadUInt16();
            _characteristics = reader.ReadUInt16();

            Characteristics = (IMAGE_FILE_Characteristics)_characteristics;
            MachineName = (MachineName)_machine;
        }
    }

    class _IMAGE_OPTIONAL_HEADER
    {
        public const int IMAGE_NUMBEROF_DIRECTORY_ENTRIES = 16;
        #region Fields
        ushort Magic;
        #endregion

        #region Properties
        public IMAGE_OPTIONAL IMAGE_OPTIONAL { get; private set; }
        public _IMAGE_OPTIONAL_HEADER32 Header32 { get; private set; }
        public _IMAGE_OPTIONAL_HEADER32 Header64 { get; private set; }
        public _IMAGE_ROM_OPTIONAL_HEADER HeaderRom { get; private set; }
        #endregion

        public _IMAGE_OPTIONAL_HEADER(BinaryReader reader)
        {
            Magic = reader.ReadUInt16();
            IMAGE_OPTIONAL = (IMAGE_OPTIONAL)Magic;
            switch (IMAGE_OPTIONAL)
            {
                case IMAGE_OPTIONAL.IMAGE_NT_OPTIONAL_HDR32_MAGIC:
                    Header32 = new _IMAGE_OPTIONAL_HEADER32(reader);
                    break;
                case IMAGE_OPTIONAL.IMAGE_NT_OPTIONAL_HDR64_MAGIC:
                    Header64 = new _IMAGE_OPTIONAL_HEADER32(reader);
                    break;
                case IMAGE_OPTIONAL.IMAGE_ROM_OPTIONAL_HDR_MAGIC:
                    HeaderRom = new _IMAGE_ROM_OPTIONAL_HEADER(reader);
                    break;
                default:
                    break;
            }
        }
    }

    public class _IMAGE_DATA_DIRECTORY
    {
        public uint VirtualAddress { get; private set; }
        public uint Size { get; private set; }
        public _IMAGE_DATA_DIRECTORY(BinaryReader reader)
        {
            VirtualAddress = reader.ReadUInt32();
            Size = reader.ReadUInt32();
        }
    }

    /// <summary>
    /// This is the 32 bit optional header
    /// </summary>
    class _IMAGE_OPTIONAL_HEADER32
    {
        #region Properties
        //
        // Standard fields.
        //

        public byte MajorLinkerVersion { get; private set; }
        public byte MinorLinkerVersion { get; private set; }
        public uint SizeOfCode { get; private set; }
        public uint SizeOfInitializedData { get; private set; }
        public uint SizeOfUninitializedData { get; private set; }
        public uint AddressOfEntryPoint { get; private set; }
        public uint BaseOfCode { get; private set; }
        public uint BaseOfData { get; private set; }

        //
        // NT additional fields.
        //

        public uint ImageBase { get; private set; }
        public uint SectionAlignment { get; private set; }
        public uint FileAlignment { get; private set; }
        public ushort MajorOperatingSystemVersion { get; private set; }
        public ushort MinorOperatingSystemVersion { get; private set; }
        public ushort MajorImageVersion { get; private set; }
        public ushort MinorImageVersion { get; private set; }
        public ushort MajorSubsystemVersion { get; private set; }
        public ushort MinorSubsystemVersion { get; private set; }
        public uint Win32VersionValue { get; private set; }
        public uint SizeOfImage { get; private set; }
        public uint SizeOfHeaders { get; private set; }
        public uint CheckSum { get; private set; }
        public ushort Subsystem { get; private set; }
        public ushort DllCharacteristics { get; private set; }
        public uint SizeOfStackReserve { get; private set; }
        public uint SizeOfStackCommit { get; private set; }
        public uint SizeOfHeapReserve { get; private set; }
        public uint SizeOfHeapCommit { get; private set; }
        public uint LoaderFlags { get; private set; }
        public uint NumberOfRvaAndSizes { get; private set; }
        public _IMAGE_DATA_DIRECTORY[] DataDirectory { get; private set; }
        #endregion       

        public _IMAGE_OPTIONAL_HEADER32(BinaryReader reader)
        {
            MajorLinkerVersion = reader.ReadByte();
            MinorLinkerVersion = reader.ReadByte();
            SizeOfCode = reader.ReadUInt32();
            SizeOfInitializedData = reader.ReadUInt32();
            SizeOfUninitializedData = reader.ReadUInt32();
            AddressOfEntryPoint = reader.ReadUInt32();
            BaseOfCode = reader.ReadUInt32();
            BaseOfData = reader.ReadUInt32();

            //
            // NT additional fields.
            //

            ImageBase = reader.ReadUInt32();
            SectionAlignment = reader.ReadUInt32();
            FileAlignment = reader.ReadUInt32();
            MajorOperatingSystemVersion = reader.ReadUInt16();
            MinorOperatingSystemVersion = reader.ReadUInt16();
            MajorImageVersion = reader.ReadUInt16();
            MinorImageVersion = reader.ReadUInt16();
            MajorSubsystemVersion = reader.ReadUInt16();
            MinorSubsystemVersion = reader.ReadUInt16();
            Win32VersionValue = reader.ReadUInt32();
            SizeOfImage = reader.ReadUInt32();
            SizeOfHeaders = reader.ReadUInt32();
            CheckSum = reader.ReadUInt32();
            Subsystem = reader.ReadUInt16();
            DllCharacteristics = reader.ReadUInt16();
            SizeOfStackReserve = reader.ReadUInt32();
            SizeOfStackCommit = reader.ReadUInt32();
            SizeOfHeapReserve = reader.ReadUInt32();
            SizeOfHeapCommit = reader.ReadUInt32();
            LoaderFlags = reader.ReadUInt32();
            NumberOfRvaAndSizes = reader.ReadUInt32();

            DataDirectory = new _IMAGE_DATA_DIRECTORY[
                _IMAGE_OPTIONAL_HEADER.IMAGE_NUMBEROF_DIRECTORY_ENTRIES];

            for (int i = 0; i < DataDirectory.Length; i++)
                DataDirectory[i] = new _IMAGE_DATA_DIRECTORY(reader);
        }
    }


    class _IMAGE_ROM_OPTIONAL_HEADER
    {
        #region Properties
        public byte MajorLinkerVersion { get; private set; }
        public byte MinorLinkerVersion { get; private set; }
        public uint SizeOfCode { get; private set; }
        public uint SizeOfInitializedData { get; private set; }
        public uint SizeOfUninitializedData { get; private set; }
        public uint AddressOfEntryPoint { get; private set; }
        public uint BaseOfCode { get; private set; }
        public uint BaseOfData { get; private set; }
        public uint BaseOfBss { get; private set; }
        public uint GprMask { get; private set; }
        public uint[] CprMask { get; private set; }//[4]
        public uint GpValue { get; private set; }
        #endregion

        public _IMAGE_ROM_OPTIONAL_HEADER(BinaryReader reader)
        {
            MajorLinkerVersion = reader.ReadByte();
            MinorLinkerVersion = reader.ReadByte();
            SizeOfCode = reader.ReadUInt32();
            SizeOfInitializedData = reader.ReadUInt32();
            SizeOfUninitializedData = reader.ReadUInt32();
            AddressOfEntryPoint = reader.ReadUInt32();
            BaseOfCode = reader.ReadUInt32();
            BaseOfData = reader.ReadUInt32();
            BaseOfBss = reader.ReadUInt32();
            GprMask = reader.ReadUInt32();
            CprMask = new uint[4];//[4]
            for (int i = 0; i < CprMask.Length; i++)            
                CprMask[i] = reader.ReadUInt32();
            GpValue = reader.ReadUInt32();

        }
    }

    class _IMAGE_OPTIONAL_HEADER64
    {
        #region Properties
        public byte MajorLinkerVersion { get; private set; }
        public byte MinorLinkerVersion { get; private set; }
        public uint SizeOfCode { get; private set; }
        public uint SizeOfInitializedData { get; private set; }
        public uint SizeOfUninitializedData { get; private set; }
        public uint AddressOfEntryPoint { get; private set; }
        public uint BaseOfCode { get; private set; }
        public ulong ImageBase { get; private set; }
        public uint SectionAlignment { get; private set; }
        public uint FileAlignment { get; private set; }
        public ushort MajorOperatingSystemVersion { get; private set; }
        public ushort MinorOperatingSystemVersion { get; private set; }
        public ushort MajorImageVersion { get; private set; }
        public ushort MinorImageVersion { get; private set; }
        public ushort MajorSubsystemVersion { get; private set; }
        public ushort MinorSubsystemVersion { get; private set; }
        public uint Win32VersionValue { get; private set; }
        public uint SizeOfImage { get; private set; }
        public uint SizeOfHeaders { get; private set; }
        public uint CheckSum { get; private set; }
        public ushort Subsystem { get; private set; }
        public ushort DllCharacteristics { get; private set; }
        public ulong SizeOfStackReserve { get; private set; }
        public ulong SizeOfStackCommit { get; private set; }
        public ulong SizeOfHeapReserve { get; private set; }
        public ulong SizeOfHeapCommit { get; private set; }
        public uint LoaderFlags { get; private set; }
        public uint NumberOfRvaAndSizes { get; private set; }
        public _IMAGE_DATA_DIRECTORY[] DataDirectory { get; private set; }
        #endregion
        public _IMAGE_OPTIONAL_HEADER64(BinaryReader reader)
        {
            MajorLinkerVersion = reader.ReadByte();
            MinorLinkerVersion = reader.ReadByte();
            SizeOfCode = reader.ReadUInt32();
            SizeOfInitializedData = reader.ReadUInt32();
            SizeOfUninitializedData = reader.ReadUInt32();
            AddressOfEntryPoint = reader.ReadUInt32();
            BaseOfCode = reader.ReadUInt32();
            ImageBase = reader.ReadUInt64();
            SectionAlignment = reader.ReadUInt32();
            FileAlignment = reader.ReadUInt32();
            MajorOperatingSystemVersion = reader.ReadUInt16();
            MinorOperatingSystemVersion = reader.ReadUInt16();
            MajorImageVersion = reader.ReadUInt16();
            MinorImageVersion = reader.ReadUInt16();
            MajorSubsystemVersion = reader.ReadUInt16();
            MinorSubsystemVersion = reader.ReadUInt16();
            Win32VersionValue = reader.ReadUInt32();
            SizeOfImage = reader.ReadUInt32();
            SizeOfHeaders = reader.ReadUInt32();
            CheckSum = reader.ReadUInt32();
            Subsystem = reader.ReadUInt16();
            DllCharacteristics = reader.ReadUInt16();
            SizeOfStackReserve = reader.ReadUInt64();
            SizeOfStackCommit = reader.ReadUInt64();
            SizeOfHeapReserve = reader.ReadUInt64();
            SizeOfHeapCommit = reader.ReadUInt64();
            LoaderFlags = reader.ReadUInt32();
            NumberOfRvaAndSizes = reader.ReadUInt32();
            DataDirectory = new _IMAGE_DATA_DIRECTORY[
                _IMAGE_OPTIONAL_HEADER.IMAGE_NUMBEROF_DIRECTORY_ENTRIES];
            for (int i = 0; i < DataDirectory.Length; i++)            
                DataDirectory[i] = new _IMAGE_DATA_DIRECTORY(reader);            

        }
    }

     /// <summary>
    /// This will tell us what type of optional data we need to read in     
    /// </summary>
    enum IMAGE_OPTIONAL
    {
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10b,
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b,
        IMAGE_ROM_OPTIONAL_HDR_MAGIC = 0x107,
    }

    /// <summary>
    /// This is the characteristics of the exe file
    /// </summary>
    [Flags]
    public enum IMAGE_FILE_Characteristics
    {
        IMAGE_FILE_RELOCS_STRIPPED = 0x0001,  // Relocation info stripped from file.
        IMAGE_FILE_EXECUTABLE_IMAGE = 0x0002,  // File is executable  (i.e. no unresolved externel references).
        IMAGE_FILE_LINE_NUMS_STRIPPED = 0x0004,  // Line nunbers stripped from file.
        IMAGE_FILE_LOCAL_SYMS_STRIPPED = 0x0008,  // Local symbols stripped from file.
        IMAGE_FILE_AGGRESIVE_WS_TRIM = 0x0010,  // Agressively trim working set
        IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x0020,  // App can handle >2gb addresses
        IMAGE_FILE_BYTES_REVERSED_LO = 0x0080,  // Bytes of machine word are reversed.
        IMAGE_FILE_32BIT_MACHINE = 0x0100,  // 32 bit word machine.
        IMAGE_FILE_DEBUG_STRIPPED = 0x0200,  // Debugging info stripped from file in .DBG file
        IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = 0x0400,  // If Image is on removable media, copy and run from the swap file.
        IMAGE_FILE_NET_RUN_FROM_SWAP = 0x0800,  // If Image is on Net, copy and run from the swap file.
        IMAGE_FILE_SYSTEM = 0x1000,  // System File.
        IMAGE_FILE_DLL = 0x2000,  // File is a DLL.
        IMAGE_FILE_UP_SYSTEM_ONLY = 0x4000,  // File should only be run on a UP machine
        IMAGE_FILE_BYTES_REVERSED_HI = 0x8000, // Bytes of machine word are reversed.
    }

    /// <summary>
    /// Directory Entries
    /// </summary>
    public enum DirectoryEntries
    {
        // Directory Entries
        IMAGE_DIRECTORY_ENTRY_EXPORT = 0,   // Export Directory
        IMAGE_DIRECTORY_ENTRY_IMPORT = 1,   // Import Directory
        IMAGE_DIRECTORY_ENTRY_RESOURCE = 2,   // Resource Directory
        IMAGE_DIRECTORY_ENTRY_EXCEPTION = 3,   // Exception Directory
        IMAGE_DIRECTORY_ENTRY_SECURITY = 4,   // Security Directory
        IMAGE_DIRECTORY_ENTRY_BASERELOC = 5,   // Base Relocation Table
        IMAGE_DIRECTORY_ENTRY_DEBUG = 6,   // Debug Directory
        //IMAGE_DIRECTORY_ENTRY_COPYRIGHT      = 7,   // (X86 usage)
        IMAGE_DIRECTORY_ENTRY_ARCHITECTURE = 7,   // Architecture Specific Data
        IMAGE_DIRECTORY_ENTRY_GLOBALPTR = 8,   // RVA of GP
        IMAGE_DIRECTORY_ENTRY_TLS = 9,   // TLS Directory
        IMAGE_DIRECTORY_ENTRY_LOAD_CONFIG = 10,   // Load Configuration Directory
        IMAGE_DIRECTORY_ENTRY_BOUND_IMPORT = 11,   // Bound Import Directory in headers
        IMAGE_DIRECTORY_ENTRY_IAT = 12,   // Import Address Table
        IMAGE_DIRECTORY_ENTRY_DELAY_IMPORT = 13,   // Delay Load Import Descriptors
        IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR = 14,   // COM Runtime descriptor
    }

    /// <summary>
    /// What machine is this exe to run on.
    /// </summary>
    public enum MachineName : ushort
    {
        IMAGE_FILE_MACHINE_UNKNOWN = 0,
        IMAGE_FILE_MACHINE_I386 = 0x014c,  // Intel 386.
        IMAGE_FILE_MACHINE_R3000 = 0x0162,  // MIPS little-endian, 0x160 big-endian
        IMAGE_FILE_MACHINE_R4000 = 0x0166,  // MIPS little-endian
        IMAGE_FILE_MACHINE_R10000 = 0x0168,  // MIPS little-endian
        IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x0169,  // MIPS little-endian WCE v2
        IMAGE_FILE_MACHINE_ALPHA = 0x0184,  // Alpha_AXP
        IMAGE_FILE_MACHINE_SH3 = 0x01a2,  // SH3 little-endian
        IMAGE_FILE_MACHINE_SH3DSP = 0x01a3,
        IMAGE_FILE_MACHINE_SH3E = 0x01a4,  // SH3E little-endian
        IMAGE_FILE_MACHINE_SH4 = 0x01a6,  // SH4 little-endian
        IMAGE_FILE_MACHINE_SH5 = 0x01a8,  // SH5
        IMAGE_FILE_MACHINE_ARM = 0x01c0,  // ARM Little-Endian
        IMAGE_FILE_MACHINE_THUMB = 0x01c2,  // ARM Thumb/Thumb-2 Little-Endian
        IMAGE_FILE_MACHINE_ARMNT = 0x01c4,  // ARM Thumb-2 Little-Endian
        IMAGE_FILE_MACHINE_AM33 = 0x01d3,
        IMAGE_FILE_MACHINE_POWERPC = 0x01F0,  // IBM PowerPC Little-Endian
        IMAGE_FILE_MACHINE_POWERPCFP = 0x01f1,
        IMAGE_FILE_MACHINE_IA64 = 0x0200,  // Intel 64
        IMAGE_FILE_MACHINE_MIPS16 = 0x0266,  // MIPS
        IMAGE_FILE_MACHINE_ALPHA64 = 0x0284,  // ALPHA64
        IMAGE_FILE_MACHINE_MIPSFPU = 0x0366,  // MIPS
        IMAGE_FILE_MACHINE_MIPSFPU16 = 0x0466,  // MIPS
        IMAGE_FILE_MACHINE_AXP64 = IMAGE_FILE_MACHINE_ALPHA64,
        IMAGE_FILE_MACHINE_TRICORE = 0x0520,  // Infineon
        IMAGE_FILE_MACHINE_CEF = 0x0CEF,
        IMAGE_FILE_MACHINE_EBC = 0x0EBC,  // EFI Byte Code
        IMAGE_FILE_MACHINE_AMD64 = 0x8664,  // AMD64 (K8)
        IMAGE_FILE_MACHINE_M32R = 0x9041,  // M32R little-endian
        IMAGE_FILE_MACHINE_CEE = 0xC0EE,
    }
}
