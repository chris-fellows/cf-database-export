using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFDatabaseExport.Controls
{
    public partial class ListItemsUserControl : UserControl
    {
        public ListItemsUserControl()
        {
            InitializeComponent();
        }

        internal List<LocalNameValuePair> GetSelectedItems(List<LocalNameValuePair> myList)
        {
            return GetCheckedItems(this.lvwList, myList);
        }

        private List<LocalNameValuePair> GetCheckedItems(ListView listView, List<LocalNameValuePair> myList)
        {
            List<LocalNameValuePair> list = new List<LocalNameValuePair>();
            for (int index = 0; index < listView.Items.Count; index++)
            {
                if (listView.Items[index].Checked)
                {
                    list.Add(myList[index]);
                }
            }
            return list;
        }
    }
}
