using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Reflection;

namespace MemoryPatch
{   
    public partial class FrmMain : Form
    {
        #region Fields
        private SelectProcess frmSelect = new SelectProcess();        
        private MemoryAccess _processAccess;
        
        private Dictionary<int, SavedAddress> _savedAddresses = 
            new Dictionary<int, SavedAddress>();
        private int _lastAddress = -1;

        private Dictionary<string, List<DataGridViewRow>> _rows = 
            new Dictionary<string, List<DataGridViewRow>>();        
        private ContextMenu _dataTypeMenu = new ContextMenu();
        private string _lastLoaded;
        
        private ConfigData _config;

        public AddressData _saveData = new AddressData();
        public AddressManager _runTimeData;
        #endregion       

        private void SetNewProcess(Process process)
        {
            SetNewProcess(process, process.MainModule);
        }

        private void SetNewProcess(Process process, ProcessModule module)
        {           
            if (process != null && module != null)
            {
                //save the process and module
                _processAccess = new MemoryAccess(process, module);                

                //show what process is selected
                lbActiveProcess.Text = _processAccess.ProcessName + " (" + _processAccess.ModuleName + ")";                
                searchControl1.EnableSearch(_processAccess);
                memoryPatchControl1.EnableMemoryAccess(_processAccess);
            }
            else
            {
                lbActiveProcess.Text = "None";                
            }
        }

        #region Load
        public FrmMain()
        {
            InitializeComponent();  
            //ProcessStartInfo info = new ProcessStartInfo(@"C:\Documents and Settings\Shawn200\My Documents\My Downloads\snes\zsnesw.exe");
            //_process = Process.Start(info);
            

            //_lockUpdate = new Thread(new ThreadStart(delegate()
            //    {
            //        while (true)
            //        {
            //            lock (_lock)
            //            {
            //                foreach (LockData locked in _lockList.Values)
            //                {
            //                    if(_seracher != null)
            //                        _processAccess.WriteValue(locked.Address, locked.Data);
            //                }
            //            }
            //            Thread.Sleep(50);
            //        }
            //    }));
            //_lockUpdate.IsBackground = true;
            //_lockUpdate.Name = "Lock Memory";
            //_lockUpdate.Start();                       
        }

        private void LoadConfig()
        {
            Stream stream = null;
            try
            {
                XmlSerializer objXmlSer = new XmlSerializer(typeof(ConfigData));
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                stream = File.Open(path + "\\config.xml", FileMode.Open);
                _config = (ConfigData)objXmlSer.Deserialize(stream);
                stream.Close();

                Left = _config.X;
                Top = _config.Y;
                Width = _config.Width;
                Height = _config.Height;
            }
            catch (Exception ex)
            {
                _config = new ConfigData();
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        private void SaveConfig()
        {
            Stream stream = null;
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string file = path + "\\config.xml";
            try
            {
                
                XmlSerializer objXmlSer = new XmlSerializer(typeof(ConfigData));
                if (File.Exists(file))
                    File.Delete(file);

                stream = File.Open(file, FileMode.OpenOrCreate);
                if (WindowState == FormWindowState.Normal)
                {
                    _config.X = Left;
                    _config.Y = Top;
                    _config.Width = Width;
                    _config.Height = Height;
                }
                objXmlSer.Serialize(stream, _config);
                stream.Close();
            }
            catch (Exception ex)
            {
                _config = new ConfigData();
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }     

        private void Form1_Load(object sender, EventArgs e)
        {
            //cboSearch.Items.Add(SearchType.Excat.ToString());            
            //cboSearch.Items.Add(SearchType.UnKnown.ToString());

            //cboNextSearch.Items.Add(SearchType.Excat.ToString());
            //cboNextSearch.Items.Add(SearchType.HasChanged.ToString());
            //cboNextSearch.Items.Add(SearchType.HasNotChanged.ToString());
            //cboNextSearch.Items.Add(SearchType.HasIncreased.ToString());
            //cboNextSearch.Items.Add(SearchType.HasDecreased.ToString());
            //cboNextSearch.Items.Add(SearchType.HasDecreasedBy.ToString());
            //cboNextSearch.Items.Add(SearchType.HasIncreasedBy.ToString());

            //string[] datatypes = Enum.GetNames(typeof(DataType));
            //foreach (string type in datatypes)
            //{
            //    cboDataType.Items.Add(type);
            //    _dataTypeMenu.MenuItems.Add(type);
            //}

            //cboDataType.SelectedIndex = 0;
            //cboSearch.SelectedIndex = 0;
            //cboNextSearch.SelectedIndex = 0;

            LoadConfig();
        }
        #endregion  

        #region Selecting Process

        private void FrmSelect_OnProcessSelected(object sender, ProcessEventArgs e)
        {
            lbActiveProcess.Text = e.Process.ProcessName;
            SetNewProcess(e.Process, e.Module);
        }

        private void btnSelectProcess_Click(object sender, EventArgs e)
        {
            SelectProcess();
        }

        private void SelectProcess()
        {
            if (frmSelect == null || frmSelect.IsDisposed)
                frmSelect = new SelectProcess();

            if (frmSelect.ShowDialog(this) == DialogResult.OK)
            {
                lbActiveProcess.Text = frmSelect.Args.Process.ProcessName;

                SetNewProcess(frmSelect.Args.Process, frmSelect.Args.Module);
            }
        }

        #endregion                               

        #region Data Accepted Buttons
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            //lock (_lock)
            //{                
            //    _lockList.Clear();
            //}
            //dataAccepted.Rows.Clear();
            //_savedAddresses.Clear();
        }
       
      
        #endregion

        private void btnRun_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = _config.LastRunDir;
            open.Filter = "*.exe|*.exe";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _config.LastRunDir = Path.GetDirectoryName(open.FileName);
                    ProcessStartInfo info = new ProcessStartInfo(open.FileName);                    
                    SetNewProcess(Process.Start(info));                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening try again.");
                    if(_processAccess != null)
                        _processAccess.KillProcess();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("Would you like to save first?", "Save?", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                if (!SaveDialog())
                    e.Cancel = true;
            }
            else if (res == DialogResult.No)
            {
                //just close
            }
            else if (res == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void searchControl1_OnAddressSelected(object sender, AddressFoundEventArgs e)
        {
            SavedAddress savedAddress = new SavedAddress(false, "Default",
                e.AddressFound.Address, e.AddressFound.DataType);

            memoryPatchControl1.AddAddress(savedAddress);

        }

        private void saveAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDialog();
        }

        private bool SaveDialog()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.xml|*.xml";
            if (!string.IsNullOrEmpty(_lastLoaded))
            {
                dlg.InitialDirectory = Path.GetDirectoryName(_lastLoaded);
                dlg.FileName = Path.GetFileName(_lastLoaded);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _lastLoaded = dlg.FileName;
                memoryPatchControl1.Save(dlg.FileName);
                _config.SavedFilesPath = Path.GetDirectoryName(dlg.FileName);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void openAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xml|*.xml";
            if (!string.IsNullOrEmpty(_lastLoaded))
            {
                dlg.InitialDirectory = Path.GetDirectoryName(_lastLoaded);
                dlg.FileName = Path.GetFileName(_lastLoaded);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {                
                _lastLoaded = dlg.FileName;
                memoryPatchControl1.Open(dlg.FileName);
            }
        }

        private void saveSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.xml|*.xml";          

            if (dlg.ShowDialog() == DialogResult.OK)
            {               
                searchControl1.Save(dlg.FileName);
                _config.SavedFilesPath = Path.GetDirectoryName(dlg.FileName);
            }
        }

        private void openSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xml|*.xml";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                searchControl1.Open(dlg.FileName);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.txt|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                memoryPatchControl1.Import(dlg.FileName);
            }
        }

        private void selectProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectProcess();
        }       
    }

    [XmlRoot("Config")]
    [Serializable]
    public class ConfigData
    {

        [XmlAttribute("x")]
        public int X;
        [XmlAttribute("y")]
        public int Y;
        [XmlAttribute("width")]
        public int Width;
        [XmlAttribute("height")]
        public int Height;
        [XmlAttribute("lastRunDir")]
        public string LastRunDir;
        [XmlAttribute("saveFilePath")]
        public string SavedFilesPath;
    }
}
