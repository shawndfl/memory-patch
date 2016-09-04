using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using MemoryManager;
using MemoryManager.Search;
using MemoryManager.Util;

namespace MemoryPatch
{
    [DefaultEvent("OnAddressSelected")]
    public partial class SearchControl : UserControl, ISearchListener
    {
        private MemoryAccess _access;
        private ISearcher _seracher;
        private SearchContext _currentSearchData;

        public event EventHandler<AddressFoundEventArgs> OnAddressSelected;

        public SearchControl()
        {
            InitializeComponent();

            cboSearch.Items.Add(SearchType.Excat.ToString());
            cboSearch.Items.Add(SearchType.UnKnown.ToString());          

            cboNextSearch.Items.Add(SearchType.Excat.ToString());
            cboNextSearch.Items.Add(SearchType.HasChanged.ToString());
            cboNextSearch.Items.Add(SearchType.HasNotChanged.ToString());
            cboNextSearch.Items.Add(SearchType.HasIncreased.ToString());
            cboNextSearch.Items.Add(SearchType.HasDecreased.ToString());
            cboNextSearch.Items.Add(SearchType.HasDecreasedBy.ToString());
            cboNextSearch.Items.Add(SearchType.HasIncreasedBy.ToString());           

            string[] datatypes = Enum.GetNames(typeof(DataType));
            foreach (string type in datatypes)
            {
                cboDataType.Items.Add(type);                
            }

            cboDataType.SelectedIndex = 0;
            cboSearch.SelectedIndex = 0;
            cboNextSearch.SelectedIndex = 0;
        }
        
        public void EnableSearch(MemoryAccess access)
        {
            if (access == null)
                throw new ArgumentNullException("access");

            _access = access;

            //create a searcher
            //_seracher = new SearcherImp(_access, this);            
            _seracher = SearchManager.CreateSearcher();
            _seracher.SetSearchListener(this);

            groupFirstSearch.Enabled = true;
            groupFound.Enabled = true;
            groupNextSearch.Enabled = true;
            groupSearching.Enabled = true;
        }
        
        #region Searching
        private void btnFirstSearch_Click(object sender, EventArgs e)
        {
            //get search type
            SearchType searchType = (SearchType)Enum.Parse(typeof(SearchType), cboSearch.Text);
            if (searchType == SearchType.Excat && txtValue.Text == "")
            {
                MessageBox.Show("Invalid input");
                return;
            }

            //get data type
            DataType datatype = (DataType)Enum.Parse(typeof(DataType), cboDataType.Text);

            ResetSearchData();

            //create search context            
            ISearchContext context = SearchManager.CreateSearchContext();
            context.ConfigureNextSearch(Parser.ToBytes(txtValue.Text, datatype), searchType, datatype, new List<string>());

            //search
            _seracher.Search(context);

            //stop update timer
            timer1.Enabled = false;
        }

        private void btnNextSearch_Click(object sender, EventArgs e)
        {
            if (_seracher != null)
            {
                //get search type
                SearchType searchType = (SearchType)Enum.Parse(typeof(SearchType), cboNextSearch.Text);
                if ((searchType == SearchType.Excat ||
                    searchType == SearchType.HasDecreasedBy ||
                    searchType == SearchType.HasIncreasedBy)
                    && txtNextVal1.Text == "")
                {
                    MessageBox.Show("Invalid input");
                    return;
                }

                //get data type
                DataType datatype = _currentSearchData.DataType;

                ResetSearchData();

                //set context
                ISearchContext context = _seracher.SearchContext;
                context.ConfigureNextSearch(Parser.ToBytes(txtValue.Text, datatype), searchType, datatype, new List<string>());

                //search
                _seracher.Search(context);

                //stop update timer
                timer1.Enabled = false;
            }
        }

        private void ResetSearchData()
        {
            dataResults.Rows.Clear();
            lbFoundCount.Text = "0";
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            _seracher.CancelSearch();
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnFirstSearch_Click(sender, e);
            }
        }

        private void txtNextVal1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnNextSearch_Click(sender, e);
            }
        }
        #endregion       

        #region Search Events
        public void OnProgressChange(SearchUpdateEventArgs e)
        {
            pgSearching.Value = e.PrecentDone;
            lbFoundCount.Text = e.AddressFoundCount.ToString();

            if (e.PrecentDone == 100)
            {
                groupNextSearch.Enabled = true;
                lbDataType.Text = _currentSearchData.DataType.ToString();
                pgSearching.Value = 0;

                //start timer to refesh addresses
                timer1.Enabled = true;
                timer1.Interval = 1000;
            }

        }

        public void OnValueFound(AddressFoundEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();            
            row.CreateCells(dataResults, string.Format("{0:X000000}", e.AddressFound.Address),
                                        e.AddressFound.GetStringValue(),
                                        e.AddressFound.DataType);
            row.Tag = e;
            dataResults.Rows.Add(row);
        }

        #endregion      

        private void dataResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (OnAddressSelected != null && dataResults.Rows[e.RowIndex].Tag != null)
                    OnAddressSelected(this, dataResults.Rows[e.RowIndex].Tag as AddressFoundEventArgs);
            }

            //string address = dataResults.Rows[e.RowIndex].Cells[0].Value as string;
            //int addressInt = Int32.Parse(address, NumberStyles.AllowHexSpecifier);

            //DataType type = (DataType)dataResults.Rows[e.RowIndex].Cells[2].Value;

            //SavedAddress savedAddress = new SavedAddress(false, "Default", addressInt, type);
        }

        public void Open(string file)
        {
            if (_seracher == null)
            {
                MessageBox.Show("Select a process first.");
            }
            else
            {
                //_seracher.LoadSnapShot(file);
                //_currentSearchData = _seracher.SearchContext;
            }
        }

        public void Save(string file)
        {
            if (_seracher == null)
            {
                MessageBox.Show("Select a process first.");
            }
            else
            {
                //_seracher.SaveSnapShot(file);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataResults.Rows)
            {
                AddressFoundEventArgs ags = row.Tag as AddressFoundEventArgs;
                if (ags == null)
                    continue;

                row.Cells[1].Value = _access.ReadMemoryAsString(ags.AddressFound.Address, 
                    ags.AddressFound.DataType, ags.AddressFound.DataLengthInBytes);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataResults.Rows.Clear();
        }

        #region IInvoke Members

        public void InvokeMethod(Delegate method)
        {
            this.Invoke(method);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        #endregion
    }
}
