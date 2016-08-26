using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryPatch
{
    class AddressSorter : IComparer
    {       
        
        /// <summary>
        /// The column to sort by. NO_SORT will sort by groups
        /// </summary>
        public int Column
        {
            get; set;
        }

        /// <summary>
        /// The state of the sorter
        /// </summary>
        public enum SorterState
        {            
            Assend,
            Desend
        }

        /// <summary>
        /// The state of the sorter.
        /// </summary>
        public SorterState State { get; set; }

        public AddressSorter()
        {
                        
        }

        /// <summary>
        /// Cycle the state from assend to desend to none.
        /// </summary>
        public void CycleState()
        {
            switch (State)
            {
                case SorterState.Assend:
                    State = SorterState.Desend;
                    break;
                case SorterState.Desend:
                    State = SorterState.None;
                    break;                
                default:
                    State = SorterState.Assend;
                    break;
            }
        }

        /// <summary>
        /// Does the compare. Object must be type of ListViewItem
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            ListViewItem item1 = x as ListViewItem;
            ListViewItem item2 = y as ListViewItem;

            if (item1 == null || item2 == null)
            {
                throw new Exception("Wrong type must be subclass of " + typeof(System.Windows.Forms.ListView).Name);
            }

            int modifier = 1;
            switch (State)
            {

                case SorterState.Assend:
                    modifier = 1;
                    break;
                case SorterState.Desend:
                    modifier = -1;
                    break;                
            }

            int subItem = Column;

            //check the range of the column
            if (subItem >= item1.SubItems.Count || subItem >= item2.SubItems.Count || Column < 0)
                return 0;

            int value = item1.SubItems[subItem].Text.CompareTo(item2.SubItems[subItem].Text);
            return value * modifier;

        }
    }
}
