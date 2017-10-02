using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MemoryManager
{
    public class AddressCollection
    {
        public const int AddressCount = 25000;
        private List<AddressFound> _snapShot1 = new List<AddressFound>(AddressCount);
        private List<AddressFound> _snapShot2 = new List<AddressFound>(AddressCount);

        public SearchContext SearchContext;

        public List<AddressFound> CurrentList
        {
            get { return SnapShotToggle ? _snapShot1 : _snapShot2; }
        }

        public List<AddressFound> LastList
        {
            get { return SnapShotToggle ? _snapShot2 : _snapShot1; }
            set { _snapShot1 = value; }
        }


        [XmlIgnore]
        public bool SnapShotToggle = true;

        public void ResetSearch(SearchContext searchContext)
        {
            SearchContext = searchContext;
            _snapShot1.Clear();
            _snapShot2.Clear();
            SnapShotToggle = true;
        }

        public void StartNextSearch()
        {
            SnapShotToggle = !SnapShotToggle;
            CurrentList.Clear();
        }
    }
}
