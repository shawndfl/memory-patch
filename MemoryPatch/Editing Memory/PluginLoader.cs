using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using MemoryManager;

namespace MemoryPatch.Editing_Memory
{
    public partial class PluginLoader : UserControl
    {
        #region Fields
        private string _pluginPath;
        private string _pluginType;
        private Assembly _assembly;
        private PluginManager _pluginManager;
        private AddressManager _addressManager;
        #endregion

        #region Constructor
        public PluginLoader()
        {
            InitializeComponent();
            if(FrmMain.Config != null)
                RefeshList(FrmMain.Config.SavedFilesPath);
        }

        #endregion

        public void Init(PluginManager pluginManager, AddressManager addressManager)
        {         
            _pluginManager = pluginManager;
            _addressManager = addressManager;
            try
            {
                if (!string.IsNullOrEmpty(addressManager.PluginPath) &&
                    !string.IsNullOrEmpty(addressManager.PluginType))
                {
                    _assembly = Assembly.LoadFile(addressManager.PluginPath);
                    Type pluginType = _assembly.GetType(addressManager.PluginType);

                    LoadPlugin(pluginType);                    
                }
                else
                {
                    groupLoadPlugin.BringToFront();
                    groupLoadPlugin.Enabled = true;                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading plugin " + addressManager.PluginPath + 
                    " type " + addressManager.PluginType + "\n" + ex.Message);
            }
        }

        #region Control Events
        private void btnOpenPath_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = FrmMain.Config.SavedFilesPath;            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                RefeshList(dlg.SelectedPath);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int i = lstTypes.SelectedIndex;
                if (i > -1)
                {
                    Type pluginType = _assembly.GetType(lstTypes.Items[i] as string);
                    LoadPlugin(pluginType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Plugin " + ex.Message);
            }
        }

        private void LoadPlugin(Type pluginType)
        {
            try
            {

                _addressManager.PluginPath = _assembly.Location;
                _addressManager.PluginType = pluginType.FullName;

                IPlugin plugin = Activator.CreateInstance(pluginType) as IPlugin;
                if (plugin != null)
                {
                    groupPlugin.BringToFront();
                    groupPlugin.Controls.Clear();
                    groupPlugin.Controls.Add(plugin.Control);

                    plugin.Init(_pluginManager);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Plugin " + ex.Message);
            }
        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RefeshList(txtPath.Text);
            }
        }

        private void lstPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lstPlugins.SelectedIndex;
            if (i > -1)
            {
                string fileName = lstPlugins.Items[i] as string;
                string fullPath = _pluginPath + fileName;

                lstTypes.Items.Clear();
                _assembly = Assembly.LoadFile(fullPath);

                Type[] types = _assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.GetInterface(typeof(IPlugin).FullName) != null)
                    {
                        lstTypes.Items.Add(type.FullName);
                    }
                }
            }
        }

        private void PluginLoader_Load(object sender, EventArgs e)
        {
            RefeshList(txtPath.Text);
        }
        #endregion

        private void RefeshList(string directory)
        {
            if (string.IsNullOrEmpty(directory))
                return;

            if (!directory.EndsWith("\\"))
                directory += "\\";
            _pluginPath = directory;
            txtPath.Text = _pluginPath;
            try
            {
                lstPlugins.Items.Clear();
                lstTypes.Items.Clear();

                string[] plugins = Directory.GetFiles(_pluginPath, "*.dll");

                foreach (string file in plugins)                
                    lstPlugins.Items.Add(Path.GetFileName(file));
                
                if (lstPlugins.Items.Count > 0)
                    lstPlugins.SelectedIndex = 0;
            }
            catch (DirectoryNotFoundException)
            {
                lstPlugins.Items.Clear();
                lstTypes.Items.Clear();
            }            
        }

    }
}
