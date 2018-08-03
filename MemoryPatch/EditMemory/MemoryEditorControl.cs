using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MemoryManager;

namespace MemoryPatch.EditMemory
{
    public partial class MemoryEditorControl : UserControl, IMemoryControl
    {
        private IMemoryAccess _access;

        public MemoryEditorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enables the memory access.
        /// </summary>
        /// <param name="access">The access.</param>
        public void EnableMemoryAccess(IMemoryAccess access)
        {
            if (access == null)
                throw new ArgumentNullException("access");

            _access = access;            
        }

        private void chkLocked_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void Import(string file)
        {
            throw new NotImplementedException();
        }

        public void Save(string file)
        {
            throw new NotImplementedException();
        }

        public void Open(string file)
        {
            throw new NotImplementedException();
        }

        public void AddAddress(SavedAddress savedAddress)
        {
            throw new NotImplementedException();
        }
    }
}
