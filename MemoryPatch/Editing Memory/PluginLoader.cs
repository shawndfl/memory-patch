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

namespace MemoryPatch.Editing_Memory
{
    public partial class PluginLoader : UserControl
    {
        private string _pluginPath;
        private string _pluginType;

        public PluginLoader()
        {
            InitializeComponent();
            RefeshList(FrmMain.Config.SavedFilesPath);
        }

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

        }

        private void txtPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                RefeshList(txtPath.Text);
            }
        }

        private void RefeshList(string directory)
        {
            _pluginPath = directory;
            txtPath.Text = _pluginPath;
            try
            {
                lstPlugins.Items.Clear();
                lstTypes.Items.Clear();

                string[] plugins = Directory.GetDirectories(_pluginPath, "*.dll");
                
                lstPlugins.Items.AddRange(plugins);
                if (lstPlugins.Items.Count > 0)
                    lstPlugins.SelectedIndex = 0;
            }
            catch (DirectoryNotFoundException)
            {
                
            }            
        }

        private void lstPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lstPlugins.SelectedIndex;
            if (i > -1)
            {
                lstTypes.Items.Clear();
                Assembly assembly = Assembly.LoadFile(_pluginPath);
                lstTypes.Items.AddRange(assembly.GetTypes());
            }
        }
    }
}
