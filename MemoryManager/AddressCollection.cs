using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MemoryManager
{
    /// <summary>
    /// Used to read from a file
    /// </summary>
    internal class AddressReader: IDisposable
    {
        private BinaryReader file;
        public String Filename
        {
            get; private set;
        }

        public class Block
        {
            public long baseAddress;
            public byte[] data;
        }

        public AddressReader(string filename)
        {
            Filename = filename;            
        }

        /// <summary>
        /// Reads a block. 
        /// </summary>
        /// <returns>If we are at the end of the
        /// stream this will return null otherwise return the block</returns>
        public Block Next()
        {
            if(file == null)
                file = new BinaryReader(new StreamReader(Filename).BaseStream);

            Block block = new Block();
            try
            {
                // Read the address
                block.baseAddress = file.ReadInt64();
                // Read the block data
                int size = file.ReadInt32();
                block.data = file.ReadBytes(size);

            }
            catch (EndOfStreamException ex)
            {
                return null;
            }
            return block;
        }

        public void Dispose()
        {
            if (file != null)
                file.Close();

            file = null;
        }

    }

    /// <summary>
    /// Used to write found addresses to a file
    /// </summary>
    internal class AddressWriter: IDisposable
    {        
        private BinaryWriter file;             

        public String Filename
        {
            get; private set;
        }

        public AddressWriter(string filename)
        {
            Filename = filename;

            //delete the file so we can write to it
            if (File.Exists(Filename))
                File.Delete(Filename);

            file = new BinaryWriter(new StreamWriter(Filename).BaseStream);
        }      

        public void WriteRegion(long address, byte[] buffer, int size)
        {
            // Write the base address from memory
            file.Write(address);

            // Write the size of the buffer
            file.Write(size);

            //write the current value            
            file.Write(buffer, 0, size);
        }        

        public void Dispose()
        {
            if (file != null)
                file.Close();

            file = null;
        }      
    }
        
    internal class AddressCollection: IDisposable
    {               
        private AddressWriter _writer;
        private AddressReader _reader;
        private readonly string FILE1 = "mem1.bin";
        private readonly string FILE2 = "mem2.bin";

        /// <summary>
        /// Gets the search context
        /// </summary>
        private SearchContext _searchContext;

        /// <summary>
        /// Gets the search context. This is used so we can pass 
        /// the context into the search threads.
        /// </summary>
        /// <returns></returns>
        public SearchContext GetSearchContext()
        {
            return _searchContext;
        }

        public bool UseAddressWriter
        {
            get { return _writer != null; }
        }      

        public AddressWriter AddressWriter
        {
            get
            {
                return _writer;
            }
        }

        public AddressReader AddressReader
        {
            get
            {
                return _reader;
            }
        }

        public AddressCollection()
        {
            _writer = new AddressWriter(FILE1);
            _reader = new AddressReader(FILE2);
        }        
      

        /// <summary>
        /// Resets the search with a new context.
        /// </summary>
        /// <param name="searchContext"></param>
        public void ResetSearch(SearchContext searchContext)
        {
            _searchContext = searchContext;

            //disposes the reader and writer
            if (_writer != null)
                _writer.Dispose();
            if (_reader != null)
                _reader.Dispose();

            //flip the reader and writer
            _writer = new AddressWriter(FILE1);
            _reader = new AddressReader(FILE2);
        }

        /// <summary>
        /// Used to update the context so the next search can start.
        /// It also switch the read and write file to compare values to memory.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="optValue"></param>
        public void StartNextSearch(SearchType type, string optValue)
        {
            _searchContext.SearchType = type;
            _searchContext.SetValue(optValue);

            //store the paths
            String newWritePath = _reader.Filename;
            String newReadPath = _writer.Filename;

            //disposes the reader and writer
            if (_writer != null)
                _writer.Dispose();
            if (_reader != null)
                _reader.Dispose();

            //flip the reader and writer
            _writer = new AddressWriter(newWritePath);
            _reader = new AddressReader(newReadPath);

        }

        public void Dispose()
        {
            if (_writer != null)
                _writer.Dispose();
            if (_reader != null)
                _reader.Dispose();
        }
    }
}
