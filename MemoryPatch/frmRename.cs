using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryPatch
{
    /// <summary>
    /// This is used to rename items
    /// </summary>
    public partial class frmRename : Form
    {
        public String NewName { get; private set; }

        public frmRename()
        {
            InitializeComponent();
        }

        public frmRename(String initalName)
        {
            NewName = initalName;

            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NewName = txtNewName.Text;
        }
    }
}
