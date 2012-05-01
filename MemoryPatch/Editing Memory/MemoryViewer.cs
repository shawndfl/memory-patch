using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HexViewer;
using MemoryManager;

namespace MemoryPatch
{
    public partial class MemoryViewer : Form
    {
        private MemoryStream _stream;

        private int streamSz = 5000;
        private MemoryAccess _access;
        private int _baseOffset;

        public MemoryViewer()
        {
            InitializeComponent();
            hexWindow1.OnValueChange += new EventHandler<HexViewer.OffsetArgs>(HexWindow1_OnValueChange);
            hexWindow1.OnOffsetChange += new EventHandler<HexViewer.OffsetArgs>(HexWindow1_OnOffsetChange);
        }

        private void HexWindow1_OnOffsetChange(object sender, HexViewer.OffsetArgs e)
        {
            txtAddress.Text = string.Format("{0:X0}", e.Offset + _baseOffset);
        }

        private void HexWindow1_OnValueChange(object sender, HexViewer.OffsetArgs e)
        {
            _access.WriteValue(_baseOffset + (int)e.Offset, e.Value);                   
        }

        public void StartAddress(int address, MemoryAccess access)
        {            
            if (_stream != null)
                _stream.Close();

            _access = access;

            _baseOffset = address - streamSz / 2;
            byte[] data = _access.ReadMemoryAsBytes(_baseOffset, streamSz); 
            _stream = new MemoryStream(data);
            hexWindow1.SetStream(_stream);

            hexWindow1.Offset = address - _baseOffset;
        }

        private void MemoryViewer_Load(object sender, EventArgs e)
        {
            string[] names = Enum.GetNames(typeof(DisplayFormat));
            foreach (string name in names)
            {
                cboDataType.Items.Add(name);
            }
            cboDataType.SelectedIndex = 0;
        }

        private void cboDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hexWindow1.DisplayFormat = (DisplayFormat)Enum.Parse(typeof(DisplayFormat), cboDataType.Text);
        }

        private void txtSelectOffset_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (txtSelectOffset.Text.StartsWith("0x"))
                    {
                        string val = txtSelectOffset.Text.Remove(0, 2);
                        hexWindow1.Offset = long.Parse(val, System.Globalization.NumberStyles.AllowHexSpecifier);
                    }
                    else
                        hexWindow1.Offset = long.Parse(txtSelectOffset.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Input a number of 0x prefix for hex\n" + ex.Message);
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    hexWindow1.Find(txtSearch.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Search Error: " + ex.Message);
                }
            }
        }
    }
}
