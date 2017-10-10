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
            _processes.Clear();            
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
            if (_process != null)
            {
                this.DialogResult = DialogResult.OK;
                Args = new ProcessEventArgs(_process);              

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
      
        private void lstProcess_DoubleClick(object sender, EventArgs e)
        {
            if (lstProcess.SelectedIndex != -1)
                Accept();
        }       
    }

    public class ProcessEventArgs: EventArgs
    {
        public Process Process {get; private set;}        

        public ProcessEventArgs(Process process)
        {
            Process = process;            
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
