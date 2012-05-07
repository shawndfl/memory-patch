using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MemoryManager;

namespace X86Disassemble
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    using (PortableExe exe = new PortableExe())
            //    {
            //        exe.OpenFile(dlg.FileName);
            //        MessageBox.Show(exe.Read());
            //    }
            //}

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string testExe = @"C:\Users\Shawn\Downloads\procexp.exe";
            using (PortableExe exe = new PortableExe())
            {
                exe.OpenFile(testExe, false);
                exe.Read();
            }
        }
    }
}
