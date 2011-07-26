using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HexViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hexWindow1.OnOffsetChange += new EventHandler<OffsetArgs>(HexWindow_OnOffsetChange);
        }

        private void HexWindow_OnOffsetChange(object sender, OffsetArgs e)
        {
            lbOffset.Text = "Offset: " + e.Offset;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {            
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileAttributes attr = File.GetAttributes(dlg.FileName);
                if (attr == FileAttributes.ReadOnly)
                {                    
                    DialogResult res = MessageBox.Show("File is read only. Would you like to change it to read write?", "Change from readonly", MessageBoxButtons.YesNo);
                    if (res == DialogResult.No)
                        return;
                    else
                    {
                        File.SetAttributes(dlg.FileName, attr ^ FileAttributes.ReadOnly);
                        attr = File.GetAttributes(dlg.FileName);
                    }
                }

                hexWindow1.CloseStream();

                try
                {
                    Stream stream = File.Open(dlg.FileName, FileMode.Open, FileAccess.ReadWrite);
                    hexWindow1.SetStream(stream);
                    hexWindow1.DisplayFormat = DisplayFormat.Hex;
                    hexWindow1.OffsetFormat = OffsetFormat.Hex;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            hexWindow1.CloseStream();
        }        

        private void Form1_Load(object sender, EventArgs e)
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
