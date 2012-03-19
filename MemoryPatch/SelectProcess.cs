using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MemoryPatch
{
    public partial class SelectProcess : Form
    {        
        private List<Process> _processes =
            new List<Process>();
        private List<ProcessModule> _modules = 
            new List<ProcessModule>();
        private Process _process;
      
        public ProcessEventArgs Args;

        public SelectProcess()
        {
            InitializeComponent();
        }

        private void SelectProcess_Load(object sender, EventArgs e)
        {            
            RefeshProcesses();
        }

        private void RefeshProcesses()
        {
            lstProcess.Items.Clear();
            lstModules.Items.Clear();
            _processes.Clear();
            _modules.Clear();
            _process = null;
            Args = null;

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (chkUserOnly.Checked && process.MainWindowTitle != String.Empty)
                {                                        
                    int index = lstProcess.Items.Add(process.ProcessName);
                    _processes.Insert(index, process);
                }
                else if (!chkUserOnly.Checked)
                {
                    int index = lstProcess.Items.Add(process.ProcessName);
                    _processes.Insert(index, process);             
                }                
            }
        }

        private void lstProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lstProcess.SelectedIndex;
            if (i > -1)
            {
                try
                {
                    _process = _processes[i];
                    lstModules.Items.Clear();
                    _modules.Clear();
                    for (int x = 0; x < _process.Modules.Count; x++)
                    {                        
                        int index = lstModules.Items.Add(_process.Modules[x].ModuleName);
                        _modules.Insert(index, _process.Modules[x]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void Accept()
        {            
            int i = lstModules.SelectedIndex;
            if (i > -1 && _process != null)
            {
                this.DialogResult = DialogResult.OK;
                Args = new ProcessEventArgs(_process, _modules[i]);              

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkUserOnly_CheckedChanged(object sender, EventArgs e)
        {
            RefeshProcesses();
        }
      
        private void lstModules_DoubleClick(object sender, EventArgs e)
        {
            if (lstModules.SelectedIndex != -1)
                Accept();
        }

        private void lstModules_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class ProcessEventArgs: EventArgs
    {
        public Process Process {get; private set;}
        public ProcessModule Module { get; private set; }

        public ProcessEventArgs(Process process, ProcessModule module)
        {
            Process = process;
            Module = module;
        }
    }

    public class ProcessList : Process
    {
        public override string ToString()
        {
            return this.ProcessName;
        }
    }
}
