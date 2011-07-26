using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HexViewer
{
    public partial class HexWindow : UserControl
    {

        #region Fields
        private BinaryReader _streamRead;
        private BinaryWriter _streamWrite;
        private int _localOffset;
        private long _baseOffset; 

        private readonly Label _template = new Label();
        private List<Label> _offsetLb = new List<Label>();
        private List<Label> _bytesLb = new List<Label>();
        private int _lastNumberOfRows;
        private bool _updating = false;
        private DisplayFormat _viewFormat;
        private OffsetFormat _offsetFormat;        
        private TextBox _edit;

        private string _lastSearchText;
        private Stack<UndoElement> _undoStack = new Stack<UndoElement>();

        public event EventHandler<OffsetArgs> OnOffsetChange;
        public event EventHandler<OffsetArgs> OnValueChange;

        #endregion

        #region Properties

        public DisplayFormat DisplayFormat
        {
            get { return _viewFormat; }
            set
            {
                if (value != _viewFormat)
                {
                    _viewFormat = value;
                    RefeshDisplay(true);
                }
            }
        }

        public OffsetFormat OffsetFormat
        {
            get { return _offsetFormat; }
            set
            {
                if (value != _offsetFormat && _streamRead != null)
                {
                    _offsetFormat = value;
                    RefeshDisplay(true);                    
                }
            }
        }

        public long BaseOffset
        {
            get { return _baseOffset; }           
        }

        public int LocalOffset
        {
            get { return _localOffset; }            
        }
     
        public long Offset
        {
            get { return _baseOffset + _localOffset; }
            set 
            {
                if (_streamRead != null)
                {
                    if (value >= _streamRead.BaseStream.Length)                    
                        value = _streamRead.BaseStream.Length - 1;                    
                    else if (value < 0)
                        value = 0;

                    if(value != Offset)
                    {                                       
                        //how far in to the row are we
                        int colOffset = (int)(value % 16);
                        long selectedRow = value - colOffset;
                        int visibleBytes = NumberOfRows * 16 - 16;

                        //change the base offset only if you need to
                        if (selectedRow > _baseOffset + visibleBytes)
                        {                            
                            _baseOffset = selectedRow - visibleBytes;
                        }
                        else if(selectedRow < _baseOffset  )
                        {
                            _baseOffset = selectedRow;
                        }
                        
                        //get the byte offset based on the Byte count
                        //for this data type
                        _localOffset = colOffset + (int)(selectedRow - _baseOffset);
                        
                        SelectOffset();
                    }
                }
            }
        }

        private int NumberOfRows
        {
            get
            {
                return Height / _template.Height;
            }
        }

        private int ColumnCount
        {
            get
            {
                switch (_viewFormat)
                {
                    case DisplayFormat.Ascii:
                    case DisplayFormat.Hex:
                    case DisplayFormat.Byte:
                    case DisplayFormat.UByte:
                        return 16;
                    case DisplayFormat.Short:
                    case DisplayFormat.UShort:
                        return 8;
                    case DisplayFormat.Float:
                    case DisplayFormat.Int:
                    case DisplayFormat.Uint:
                        return 4;
                    case DisplayFormat.Long:
                    case DisplayFormat.Double:
                        return 2;
                    default:
                        throw new Exception("unkown type " + _viewFormat);
                }
            }
        }

        private int ByteCount
        {
            get
            {
                return 16 / ColumnCount;
            }
        }
        #endregion

        #region Constructors
	    
        public HexWindow()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
            this.TabIndex = 0;

            _template.Width = 50;

            //edit used to edit file
            _edit = new TextBox();
            _edit.Visible = false;
            _edit.KeyUp += new KeyEventHandler(Edit_KeyUp);
            Controls.Add(_edit);           
        }
        
        #endregion

        #region Public Functions

        public void CloseStream()
        {
            if (_streamRead != null)
                _streamRead.BaseStream.Close();

            _undoStack.Clear();            
        }

        public void SetStream(Stream stream)
        {
            CloseStream();

            _streamRead = new BinaryReader (stream);
            if(stream.CanWrite)
                _streamWrite = new BinaryWriter(stream);

            RefeshDisplay(true);
        }

        public void Find(string searchText)
        {
            _lastSearchText = searchText;

            //start reading at the next byte
            _streamRead.BaseStream.Position = Offset+1;

            switch (_viewFormat)
            {
                case DisplayFormat.Hex:
                    {
                        byte searchValue = byte.Parse(searchText, System.Globalization.NumberStyles.AllowHexSpecifier);
                        for (long i = Offset+1; i < _streamRead.BaseStream.Length; i++)
                        {
                            byte value = _streamRead.ReadByte();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                        }
                        break;
                    }
                case DisplayFormat.Ascii:
                    {
                        //byte[] searchValue = Encoding.ASCII.GetBytes(searchText.ToCharArray());
                        //for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        //{                            
                        //    for (int j = 0; j < searchValue.Length; j++)
                        //    {
                        //        byte value = _streamRead.ReadByte();
                        //        if (value == searchValue[j])
                        //        {
                        //            Offset = i;
                        //        }
                        //    }                            
                        //}
                        break;
                    }
                case DisplayFormat.Byte:
                    {
                        sbyte searchValue = sbyte.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            sbyte value = _streamRead.ReadSByte();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                        }
                        break;
                    }
                case DisplayFormat.UByte:
                    {
                        byte searchValue = byte.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            byte value = _streamRead.ReadByte();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                        }
                        break;
                    }                    
                case DisplayFormat.Short:
                    {
                        short searchValue = short.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            short value = _streamRead.ReadInt16();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            //check every byte
                            long pos = _streamRead.BaseStream.Position;
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }
                        break;
                    }                    
                case DisplayFormat.UShort:
                    {
                        ushort searchValue = ushort.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            ushort value = _streamRead.ReadUInt16();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            long pos = _streamRead.BaseStream.Position;
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }
                        break;
                    }                    
                case DisplayFormat.Int:
                    {
                        int searchValue = int.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            int value = _streamRead.ReadInt32();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            long pos = _streamRead.BaseStream.Position;
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }

                        break;
                    }                    
                case DisplayFormat.Uint:
                    {
                        uint searchValue = uint.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            uint value = _streamRead.ReadUInt32();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            long pos = _streamRead.BaseStream.Position;
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }
                        break;
                    }                    
                case DisplayFormat.Long:
                    {
                        long searchValue = long.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            long value = _streamRead.ReadInt64();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            long pos = _streamRead.BaseStream.Position;
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }
                        break;
                    }                   
                case DisplayFormat.Float:
                    {
                        float searchValue = float.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            float value = _streamRead.ReadSingle();                                                  
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            long pos = _streamRead.BaseStream.Position;      
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }
                        break;
                    }                    
                case DisplayFormat.Double:
                    {
                        double searchValue = double.Parse(searchText);
                        for (long i = Offset + 1; i < _streamRead.BaseStream.Length; i++)
                        {
                            double value = _streamRead.ReadDouble();
                            if (value == searchValue)
                            {
                                Offset = _streamRead.BaseStream.Position - ByteCount;
                                return;
                            }
                            long pos = _streamRead.BaseStream.Position;
                            _streamRead.BaseStream.Position = pos - (ByteCount - 1); 
                        }
                        break;
                    }                    
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion

        #region Display Functions

        private void SelectOffset()
        {
            RefeshDisplay(true);           
            EditOffset(Offset, _bytesLb[_localOffset]);            
        }

        private void RefeshDisplay(bool moveScrollBar)
        {            
            //clear anything that was being edited
            _edit.Visible = false;

            //position the scrollbar
            if(moveScrollBar)         
                SetUpScrollBar();
            
            //create offset column
            CreateOffsetLabels();

            //show file contents
            ShowHexValues();
            
        }
        
        private void SetUpScrollBar()
        {
            if (_streamRead == null)
                return;

            _updating = true;

            vScrollBar1.Maximum = (int)(_streamRead.BaseStream.Length / 16);            
            vScrollBar1.Value = (int)(_baseOffset / 16);

            _updating = false;

        }

        private void ShowHexValues()
        {
            if (_streamRead == null)
                return;

            _streamRead.BaseStream.Position = _baseOffset;
            int colCount = 16;//ColumnCount;

            for (int row = 0; row < NumberOfRows; row++)            
            {                
                //add cols
                for (int col = 0; col < colCount; col++)
                {
                    int index = row * 16 + col;

                    Label lb;                    
                    if (index >= _bytesLb.Count)
                    {                       
                        lb = new Label();
                        lb.Name = "Value" + index;
                        lb.Tag = index;                        
                        lb.Click += new EventHandler(lb_Click);                        
    
                        _bytesLb.Add(lb);
                        Controls.Add(lb);
                    }
                    else
                    {
                        lb = _bytesLb[index];
                    }

                    int paddingForOffset = _template.Width * 2;

                    if (ByteCount == 1)
                        lb.Width = 30;
                    else if(ByteCount == 2)
                        lb.Width = 50;
                    else if (ByteCount == 4)
                        lb.Width = 55;
                    else if (ByteCount == 8)
                        lb.Width = 55;

                    lb.Height = _template.Height;
                    lb.Top = row * _template.Height;
                    lb.Left = col * lb.Width + paddingForOffset;                    

                    //bring it back if we went from a large 
                    //data type to a small one
                    if (lb.Visible == false)
                        lb.Visible = true;

                    if (index == _localOffset)
                        lb.ForeColor = Color.Blue;
                    else
                        lb.ForeColor = Color.Black;

                    lb.Text = ReadNextElement(_viewFormat);                                        
                }

                //remove old cols
                //for (int col = colCount; col < 16; col++)
                //{
                //    int index = row * 16 + col;

                //    Label lb;
                //    if (row * colCount + col >= _bytesLb.Count)
                //    {
                //        lb = new Label();
                //        lb.Name = "Value" + index;
                //        lb.Tag = index;
                //        lb.Click += new EventHandler(lb_Click);   

                //        _bytesLb.Add(lb);
                //        Controls.Add(lb);
                //    }
                //    else
                //    {
                //        lb = _bytesLb[row * 16 + col];
                //    }

                //    lb.Visible = false;
                //}
            }
        }           

        private void EditOffset(long offset, Label lb)
        {
            if (OnOffsetChange != null)                            
                OnOffsetChange(this, new OffsetArgs(Offset, ReadBytes((int)Offset, ByteCount)));            
                      
            _edit.Left = lb.Left;
            _edit.Top = lb.Top;
            Graphics g = this.CreateGraphics();
            SizeF sz = g.MeasureString(lb.Text, lb.Font);
            _edit.Width = (int)sz.Width + 10;
            _edit.Height = lb.Height;
            _edit.Text = lb.Text;            
            _edit.Tag = offset;

            _edit.Visible = true;
            _edit.Focus();

            _edit.SelectAll();
        }        

        private string ReadNextElement(DisplayFormat displayFormat)
        {            
            //check if end of stream
            int bytesToRead = (16 / ColumnCount); 
            if (_streamRead.BaseStream.Position + bytesToRead > _streamRead.BaseStream.Length)
            {
                return "";
            }
            string retValue;
            switch (displayFormat)
            {
                case DisplayFormat.Ascii:
                    retValue = new String(Encoding.ASCII.GetChars(new byte[] { (byte)_streamRead.ReadByte() }));
                    break;
                case DisplayFormat.Hex:
                    retValue = string.Format("{0:X2}", _streamRead.ReadByte());
                    break;
                case DisplayFormat.Byte:
                    retValue = _streamRead.ReadSByte().ToString();
                    break;
                case DisplayFormat.UByte:
                    retValue = _streamRead.ReadByte().ToString();
                    break;
                case DisplayFormat.Short:
                    retValue = _streamRead.ReadInt16().ToString();
                    break;
                case DisplayFormat.UShort:
                    retValue = _streamRead.ReadUInt16().ToString();
                    break;
                case DisplayFormat.Int:
                    retValue = _streamRead.ReadInt32().ToString();
                    break;
                case DisplayFormat.Uint:
                    retValue = _streamRead.ReadUInt32().ToString();
                    break;
                case DisplayFormat.Long:
                    retValue = _streamRead.ReadInt64().ToString();
                    break;
                case DisplayFormat.Float:
                    retValue = _streamRead.ReadSingle().ToString();
                    break;
                case DisplayFormat.Double:
                    retValue = _streamRead.ReadDouble().ToString();
                    break;
                default:
                    throw new Exception("unkown type " + displayFormat);
            }

            if(bytesToRead > 1)
                _streamRead.BaseStream.Position -= bytesToRead - 1;

            return retValue;
        }

        public byte[] ReadBytes(int offset, int length)
        {
            byte[] buffer = new byte[length];
            if (offset > 0 && offset + length <= _streamRead.BaseStream.Length)
            {
                _streamRead.BaseStream.Position = offset;
                _streamRead.Read(buffer, 0, length);
            }
            return buffer;
        }

        private void CreateOffsetLabels()
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                Label lb;
                if (i >= _offsetLb.Count)
                {
                    lb = new Label();
                    lb.Top = i * _template.Height;
                    _offsetLb.Add(lb);
                    Controls.Add(lb);
                }
                else
                {
                    lb = _offsetLb[i];
                }
                                
                lb.Text = ReadNextElement(i * 16 + _baseOffset);
            }            
        }

        private string ReadNextElement(long offset)
        {
            switch (_offsetFormat)
            {
                case OffsetFormat.Dec:
                    return offset.ToString();
                case OffsetFormat.Hex:
                    return string.Format("{0:X8}", offset);                
                default:
                    throw new Exception("unkown type " + _offsetFormat);
            }

        }
        #endregion

        #region Control Events
        private void HexWindow_Resize(object sender, EventArgs e)
        {
            if(NumberOfRows > _lastNumberOfRows)
                RefeshDisplay(true);

            _lastNumberOfRows = NumberOfRows;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (_updating)
                return;

            _baseOffset = vScrollBar1.Value * 16;
            _localOffset = 0;
            RefeshDisplay(false);
        }       
        #endregion       

        #region Edit and label events
        private void lb_Click(object sender, EventArgs e)
        {
            WriteValue(Offset, _edit.Text, _viewFormat);

            Label lb = (Label)sender;
            int index = (int)lb.Tag;
            _localOffset = index;

            RefeshDisplay(false);
            EditOffset(Offset, lb);            
        }

        private void Edit_KeyUp(object sender, KeyEventArgs e)
        {
            if (_streamRead == null)
                return;

            //accept edit
            if (e.KeyCode == Keys.Return)
            {
                //edit file
                WriteValue(Offset, _edit.Text, _viewFormat);
                _edit.Visible = false;
            }
            //move right
            else if (e.KeyCode == Keys.Right && _edit.SelectionStart >= _edit.Text.Length)
            {
                WriteValue(Offset, _edit.Text, _viewFormat);
                Offset++;               
            }
            //move left
            else if (e.KeyCode == Keys.Left && _edit.SelectionStart <= 0)
            {
                WriteValue(Offset, _edit.Text, _viewFormat);
                Offset--;                
            }
            //move up
            else if (e.KeyCode == Keys.Up)
            {
                WriteValue(Offset, _edit.Text, _viewFormat);
                Offset -= 16;                
            }
            //move down
            else if (e.KeyCode == Keys.Down)
            {
                WriteValue(Offset, _edit.Text, _viewFormat);
                Offset += 16;                
            }
            //cancel edit
            else if (e.KeyCode == Keys.Escape)
            {
                _edit.Visible = false;
            }
            //undo last edit
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Z)
            {
                if (_undoStack.Count > 0)
                {
                    UndoElement element = _undoStack.Pop();
                    WriteUndo(element);                    
                }
            }
            //search next
            else if (e.KeyCode == Keys.F3)
            {
                try
                {
                    Find(_lastSearchText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Y)
            //{
            //    if (_redoStack.Count > 0)
            //    {
            //        UndoElement element = _redoStack.Pop();
            //        WriteUndo(element);                    
            //    }
            //}
        }

        private void WriteUndo(UndoElement element)
        {
            try
            {
                if (_streamWrite == null)
                    throw new Exception("Can't write to this stream");

                _streamWrite.BaseStream.Position = element.Offset;
                _streamWrite.Write(element.Data);
                
                RefeshDisplay(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WriteValue(long offset, string newValue, DisplayFormat format)
        {
            try
            {
                if (_streamWrite == null)
                    throw new Exception("Can't write to this stream");

                if (string.IsNullOrEmpty(newValue))
                    throw new Exception("Value can not be null");
                
                //set up the undo buffer
                _streamRead.BaseStream.Position = offset;
                byte[] bytesToUndo = _streamRead.ReadBytes(ByteCount);

                //set the position for writing. this needs to be done after
                //we read the bytes for undoing because they are using
                //the same stream
                _streamWrite.BaseStream.Position = offset;

                switch (format)
                {
                    case DisplayFormat.Hex:
                        {
                            if (newValue.Length > 2)
                                throw new Exception("can only convert one byte at a time.");                            

                            byte value = byte.Parse(newValue, System.Globalization.NumberStyles.AllowHexSpecifier);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Ascii:
                        {
                            if (newValue.Length > 1)
                                throw new Exception("can only convert one character at a time.");                            

                            byte value = Encoding.ASCII.GetBytes(newValue)[0];

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Byte:
                        {
                            if (newValue.Length > 3)
                                throw new Exception("can only convert one byte at a time.");                            

                            sbyte value = sbyte.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.UByte:
                        {
                            if (newValue.Length > 3)
                                throw new Exception("can only convert one byte at a time.");                            

                            byte value = byte.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Short:
                        {                            
                            short value = short.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.UShort:
                        {                            
                            ushort value = ushort.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Int:
                        {                            
                            int value = int.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Uint:
                        {                            
                            uint value = uint.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Long:
                        {                            
                            long value = long.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Float:
                        {                           
                            float value = float.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    case DisplayFormat.Double:
                        {
                            double value = double.Parse(newValue);

                            _undoStack.Push(new UndoElement(offset, bytesToUndo));
                            _streamWrite.Write(value);
                            break;
                        }
                    default:
                        throw new NotSupportedException();
                }
                
                //fire value change event
                if (OnValueChange != null)
                    OnValueChange(this, new OffsetArgs(Offset, ReadBytes((int)Offset, ByteCount)));            

                RefeshDisplay(false);
            }            
            catch (FormatException ex)
            {
                MessageBox.Show("Can't convert " + newValue + " to " + format);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        #endregion         
    }

    public enum DisplayFormat
    {
        Hex,
        Ascii,        
        Byte,
        UByte,
        Short,
        UShort,
        Int,
        Uint,       
        Long,
        Float,
        Double,
    }

    public enum OffsetFormat
    {
        Hex, 
        Dec
    }

    public class OffsetArgs: EventArgs
    {
        public long Offset{get; private set;}
        public byte[] Value { get; private set; }

        public OffsetArgs(long offset, byte[] value)
        {
            Offset = offset;
            Value = value;
        }
    }

    public class UndoElement
    {
        public long Offset { get; private set; }
        public byte[] Data { get; private set; }

        public UndoElement(long offset, byte[] data)
        {
            Offset = offset;
            Data = new byte[data.Length];
            data.CopyTo(Data, 0);
        }
    }
}
