using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryPatch
{
    public class AddressContextMenu: ContextMenu
    {



        public AddressContextMenu ()
        {
            MenuItem rename = new MenuItem("Rename...", Rename);
            MenuItems.Add(rename);
        }

        private void Rename(object sender, EventArgs e)
        {
            new frmRename();
        }
        
    }
}
