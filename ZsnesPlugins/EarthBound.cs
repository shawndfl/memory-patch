using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MemoryManager;

namespace ZsnesPlugins
{
    public partial class EarthBound : UserControl, IPlugin
    {
        #region Fields
        private PluginManager _manager;

        #endregion
        public EarthBound()
        {
            InitializeComponent();
        }

        #region IPlugin Members

        public new Control Control
        {
            get { return this; }
        }

        public void Init(PluginManager manager)
        {
            _manager = manager;            
        }

        #endregion

        private void btnMaxHp_Click(object sender, EventArgs e)
        {
            //_manager.ReadValue(
        }
    }
}
