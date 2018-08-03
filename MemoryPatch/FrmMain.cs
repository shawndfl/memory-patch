using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using MemoryManager;
using MemoryPatch.EditMemory;

namespace MemoryPatch
{
    public partial class FrmMain : Form
    {
        #region Fields
        private SelectProcess frmSelect = new SelectProcess();        
        private IMemoryAccess _processAccess;
        
        private Dictionary<int, SavedAddress> _savedAddresses = 
            new Dictionary<int, SavedAddress>();
        private int _lastAddress = -1;

        private Dictionary<string, List<DataGridViewRow>> _rows = 
            new Dictionary<string, List<DataGridViewRow>>();        
        private ContextMenu _dataTypeMenu = new ContextMenu();
        private string _lastLoaded;

        /// <summary>
        /// Access to config data
        /// </summary>
        public static ConfigData Config { get; private set; }
        
        public AddressManager _runTimeData;
        #endregion       

        private IMemoryControl MemCtrl
        {
            get
            {
                return memoryPatchControl1;
            }
        }
        
        private void SetNewProcess(Process process)
        {           
            if (process != null)
            {
                //save the process and module
                _processAccess = MemoryAccessFactory.CreateMemoryAccess(process);

                //show what process is selected
                lbActiveProcess.Text = _processAccess.ProcessName;
                searchControl1.EnableSearch(_processAccess);
                MemCtrl.EnableMemoryAccess(_processAccess);
            }
            else
            {
                lbActiveProcess.Text = "None";                
            }
        }

        #region Load
        public FrmMain()
        {
            LoadConfig();            
            InitializeComponent();             
        }

        private void LoadConfig()
        {
            Stream stream = null;
            try
            {
                XmlSerializer objXmlSer = new XmlSerializer(typeof(ConfigData));
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                stream = File.Open(path + "\\config.xml", FileMode.Open);
                Config = (ConfigData)objXmlSer.Deserialize(stream);
                stream.Close();

                Left = Config.X;
                Top = Config.Y;
                Width = Config.Width;
                Height = Config.Height;
            }
            catch (Exception ex)
            {
                Config = new ConfigData();
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        private void SetWindowState()
        {
            Left = Config.X;
            Top = Config.Y;
            Width = Config.Width;
            Height = Config.Height;
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
                    Config.X = Left;
                    Config.Y = Top;
                    Config.Width = Width;
                    Config.Height = Height;
                }
                objXmlSer.Serialize(stream, Config);
                stream.Close();
            }
            catch (Exception)
            {
                Config = new ConfigData();
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }     

        private void Form1_Load(object sender, EventArgs e)
        {           
            //LoadConfig();
            SetWindowState();
        }
        #endregion  

        #region Selecting Process

        private void FrmSelect_OnProcessSelected(object sender, ProcessEventArgs e)
        {
            lbActiveProcess.Text = e.Process.ProcessName;
            SetNewProcess(e.Process);
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
                SetNewProcess(frmSelect.Args.Process);
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
            open.InitialDirectory = Config.LastRunDir;
            open.Filter = "*.exe|*.exe";
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Config.LastRunDir = Path.GetDirectoryName(open.FileName);
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
                e.AddressFound.Address, e.AddressFound.DataType, e.AddressFound.DataLengthInBytes);

            MemCtrl.AddAddress(savedAddress);

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
                MemCtrl.Save(dlg.FileName);
                Config.SavedFilesPath = Path.GetDirectoryName(dlg.FileName);
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
            dlg.InitialDirectory = Config.SavedFilesPath;
            if (!string.IsNullOrEmpty(_lastLoaded))
            {
                dlg.InitialDirectory = Path.GetDirectoryName(_lastLoaded);
                dlg.FileName = Path.GetFileName(_lastLoaded);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {                
                _lastLoaded = dlg.FileName;
                Config.SavedFilesPath = Path.GetDirectoryName(_lastLoaded);
                MemCtrl.Open(dlg.FileName);
            }
        }

        private void saveSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.xml|*.xml";          

            if (dlg.ShowDialog() == DialogResult.OK)
            {               
                searchControl1.Save(dlg.FileName);
                Config.SavedFilesPath = Path.GetDirectoryName(dlg.FileName);
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
                MemCtrl.Import(dlg.FileName);
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
