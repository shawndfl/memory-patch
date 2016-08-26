using MemoryManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MemoryPatch
{
    /// <summary>
    /// This is used to rename items
    /// </summary>
    public partial class frmBatchCreate : Form
    {
        public String NewName { get; private set; }

        public frmBatchCreate(List<SavedAddress> addresses)
        {
            InitializeComponent();
        }

        public frmBatchCreate(String initalName)
        {
            NewName = initalName;

            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        private void frmBatchCreate_Load(object sender, EventArgs e)
        {

        }
    }
}
