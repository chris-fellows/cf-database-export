using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Data.OleDb;
using CFDatabaseExport.Models;
using CFDatabaseExport.Utilities;
using CFUtilities.Databases;

namespace CFDatabaseExport.Forms
{
    public partial class ParametersForm : Form
    {
        private OleDbDatabase _database;
        private List<QueryFunction> _queryFunctionList = new List<QueryFunction>();
        private List<ScriptFunction> _scriptFunctions = new List<ScriptFunction>();
        private Dictionary<ScriptFunction, List<LocalNameValuePair>> _itemListList = new Dictionary<ScriptFunction, List<LocalNameValuePair>>();

        public ParametersForm()
        {
            InitializeComponent();
        }

        internal ParametersForm(OleDbDatabase database, Query query, List<QueryFunction> queryFunctionList, List<ScriptFunction> scriptFunctions)
        {
            InitializeComponent();

            _database = database;
            _queryFunctionList = queryFunctionList;
            _scriptFunctions = scriptFunctions;

            InitialiseScreen(query);
        }

        private QueryFunction GetQueryFunctionForTabPage(TabPage tabPage)
        {
            return (QueryFunction)tabPage.Tag;
        }

        private void InitialiseScreen(Query query)
        {
            tabControl1.SuspendLayout();
            List<TabPage> usedTabPageList = new List<TabPage>();

            for (int index = 0; index < _scriptFunctions.Count; index++)
            {
                TabPage tabPage = null;
                ScriptFunction scriptFunction = _scriptFunctions[index];
                if (scriptFunction is ScriptFunctionSelectItem)
                {
                    ScriptFunctionSelectItem scriptFunctionSelectItem = (ScriptFunctionSelectItem)scriptFunction;
                    QueryFunctionListItem queryFunctionListItem = (QueryFunctionListItem)_queryFunctionList.FirstOrDefault(qf => qf.Name.ToUpper() == scriptFunctionSelectItem.FunctionName.ToUpper());

                    tabPage = new TabPage(scriptFunctionSelectItem.Prompt);
                    tabPage.SuspendLayout();
                    tabPage.Controls.Add(new Controls.ListItemsUserControl());
                    tabPage.ResumeLayout();

                    DisplayGetParametersFromList(scriptFunctionSelectItem, queryFunctionListItem, _database, tabPage);
                }
                
                tabControl1.TabPages.Add(tabPage);
                tabPage.Tag = scriptFunction;        // Link tab to query function
                usedTabPageList.Add(tabPage);
            }

            // Remove unused tab pages
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (usedTabPageList.Find(item => (item == tabPage)) == null)
                {
                    tabControl1.TabPages.Remove(tabPage);
                }
            }

            tabControl1.ResumeLayout();
            tabControl1.Refresh();
            tabControl1.Update();

            tabControl1.SelectedTab = tabControl1.TabPages[0];
            HandleTabSelected();
        }

        private static bool DataReaderContainsColumn(OleDbDataReader reader, string columnName)
        {
            for (int field = 0; field < reader.FieldCount; field++)
            {
                if (reader.GetName(field).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private void DisplayGetParametersFromList(ScriptFunctionSelectItem scriptfunction, QueryFunctionListItem queryFunction, 
                                                 OleDbDatabase database, TabPage tabPage)
        {
            tabPage.Text = scriptfunction.Prompt;

            // Add list items
            List<LocalNameValuePair> itemList = new List<LocalNameValuePair>();
            _itemListList.Add(scriptfunction, itemList);

            bool[] defaultSelectedList = new bool[0];

            // Get list of values to select from
            //List<NameValuePair> nameValuePairList = new List<NameValuePair>();
            using (OleDbDataReader reader = database.ExecuteReader(System.Data.CommandType.Text, queryFunction.ListSQL, System.Data.CommandBehavior.Default))
            {
                bool? containsDefaultSelectedColumn = null;
                while (reader.Read())
                {
                    itemList.Add(new LocalNameValuePair(reader.GetString(1), reader.GetValue(0)));
                    containsDefaultSelectedColumn = (containsDefaultSelectedColumn == null ? DataReaderContainsColumn(reader, "DefaultSelected") : containsDefaultSelectedColumn);
                    if (containsDefaultSelectedColumn.Value)
                    {
                        Array.Resize(ref defaultSelectedList, defaultSelectedList.Length + 1);
                        defaultSelectedList[defaultSelectedList.Length - 1] = reader.GetBoolean(reader.GetOrdinal("DefaultSelected"));
                    }
                }
                reader.Close();
            }

            ListView listView = UIUtilities.GetListView(tabPage.Controls[0]);
            ListViewItem topItem = null;
            int index = -1;
            listView.Items.Clear();
            foreach (LocalNameValuePair currentItem in itemList)
            {
                index++;
                ListViewItem item = listView.Items.Add(new ListViewItem(currentItem.Name));

                // Default selected                
                if (defaultSelectedList != null && defaultSelectedList.Length > 0)
                {
                    if (defaultSelectedList[index] && topItem == null)
                    {
                        topItem = item;
                    }
                    listView.Items[listView.Items.Count - 1].Checked = defaultSelectedList[index];
                }
                //_itemList.Add(currentItem);
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            if (topItem != null)
            {
                listView.TopItem = topItem;
            }
        }

        private TabPage GetTabPageForScriptFunction(ScriptFunction scriptFunction)
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                ScriptFunction scriptFunctionCurrent = (ScriptFunction)tabPage.Tag;
                if (scriptFunctionCurrent == scriptFunction)
                {
                    return tabPage;
                }
            }
            return null;
        }

        public List<LocalNameValuePair> GetSelectedListItems(ScriptFunction scriptFunction)
        {
            TabPage tabPage = GetTabPageForScriptFunction(scriptFunction);
            Controls.ListItemsUserControl control = (Controls.ListItemsUserControl)UIUtilities.GetControl<Controls.ListItemsUserControl>(tabPage);
            return control.GetSelectedItems(_itemListList[scriptFunction]);
        }

        //internal LocalNameValuePair GetSelectedValueGetParameterFromList(int queryFunctionIndex)
        //{
        //    QueryFunction queryFunction = _queryFunctionList[queryFunctionIndex];
        //    TabPage tabPage = GetTabPageForQueryFunction(_queryFunctionList[queryFunctionIndex]);
        //    Controls.ListItemsUserControl control = (Controls.ListItemsUserControl)UIUtilities.GetControl<Controls.ListItemsUserControl>(tabPage);
        //    return control.GetSelectedItems(_itemListList[queryFunction]).First();
        //}

        //internal List<LocalNameValuePair> GetSelectedValuesGetParametersFromList(int queryFunctionIndex)
        //{
        //    QueryFunction queryFunction = _queryFunctionList[queryFunctionIndex];
        //    TabPage tabPage = GetTabPageForQueryFunction(_queryFunctionList[queryFunctionIndex]);
        //    Controls.ListItemsUserControl control = (Controls.ListItemsUserControl)UIUtilities.GetControl<Controls.ListItemsUserControl>(tabPage);
        //    return control.GetSelectedItems(_itemListList[queryFunction]);
        //}

        //internal string GetSelectedFileGetBCPFile(int queryFunctionIndex)
        //{
        //    QueryFunction queryFunction = _queryFunctionList[queryFunctionIndex];
        //    TabPage tabPage = GetTabPageForQueryFunction(_queryFunctionList[queryFunctionIndex]);
        //    GetBCPFileUserControl control = (GetBCPFileUserControl)ControlUtilities.GetControl<GetBCPFileUserControl>(tabPage);
        //    return control.Filename;
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            string message = ValidateBeforeClose();
            if (String.IsNullOrEmpty(message))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Close");
            }
        }

        private string Validate(TabPage tabPage)
        {
            string message = "";
            QueryFunction queryFunction = (QueryFunction)tabPage.Tag;

            if (queryFunction is QueryFunctionListItem)
            {
                message = Validate(tabPage, (QueryFunctionListItem)queryFunction);
            }
            //else if (queryFunction is QueryFunctionGetParametersFromList)
            //{
            //    message = Validate(tabPage, (QueryFunctionGetParametersFromList)queryFunction);
            //}
            //else if (queryFunction is QueryFunctionGetBCPFile)
            //{
            //    message = Validate(tabPage, (QueryFunctionGetBCPFile)queryFunction);
            //}
            return message;
        }

        private List<LocalNameValuePair> GetCheckedItems(ScriptFunction scriptFunction, ListView listView)
        {
            List<LocalNameValuePair> list = new List<LocalNameValuePair>();
            List<LocalNameValuePair> myList = _itemListList[scriptFunction];

            for (int index = 0; index < listView.Items.Count; index++)
            {
                if (listView.Items[index].Checked)
                {
                    list.Add(myList[index]);
                }
            }
            return list;
        }

        /*
        private string Validate(TabPage tabPage, QueryFunctionGetParameterFromList queryFunction)
        {
            SelectListItemUserControl control = (SelectListItemUserControl)ControlUtilities.GetControl<SelectListItemUserControl>(tabPage);


            string message = "";
            //ComboBox comboBox = ControlUtilities.GetComboBox(tabPage.Controls[0]);
            return message;
        }
        */

        private string Validate(TabPage tabPage, QueryFunctionListItem queryFunction)
        {          
            Controls.ListItemsUserControl control = (Controls.ListItemsUserControl)UIUtilities.GetControl<Controls.ListItemsUserControl>(tabPage);

            string message = "";
            //ListView listView = ControlUtilities.GetListView(tabPage.Controls[0]);
            //List<NameValuePair> selectedItemList = GetCheckedItems(queryFunction, listView);       

            //List<LocalNameValuePair> selectedItemList = control.GetSelectedItems(_itemListList[queryFunction]);

            //int minItems = queryFunction.MinItems > 0 ? Convert.ToInt32(queryFunction.MinItems) : -1;
            //int maxItems = queryFunction.MaxItems > 0 ? Convert.ToInt32(queryFunction.MaxItems) : -1;
            //if (maxItems != -1 && selectedItemList.Count > maxItems)
            //{
            //    message = string.Format("You must select no more than {0} item(s)", maxItems);
            //}
            //else if (minItems != -1 && selectedItemList.Count < minItems)
            //{
            //    message = string.Format("You must select at least {0} item(s)", minItems);
            //}
            return message;
        }

        //private string Validate(TabPage tabPage, QueryFunctionGetBCPFile queryFunction)
        //{
        //    GetBCPFileUserControl control = (GetBCPFileUserControl)ControlUtilities.GetControl<GetBCPFileUserControl>(tabPage);

        //    string message = "";
        //    //TextBox textBox = ControlUtilities.GetTextBox(tabPage.Controls[0]);
        //    string file = control.Filename;
        //    if (String.IsNullOrEmpty(file))
        //    {
        //        message = "File to import is not set";
        //    }
        //    else if (!System.IO.File.Exists(file))
        //    {
        //        message = string.Format("File {0} to import does not exist", file);
        //    }
        //    return message;
        //}

        private string ValidateBeforeClose()
        {
            string message = "";
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                message = Validate(tabPage);
                if (!String.IsNullOrEmpty(message))
                {
                    break;
                }
            }
            return message;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void tstFindText_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void tstFindText_Click(object sender, EventArgs e)
        {

        }

        private void FindText()
        {
            /* TODO: Fix this
            QueryFunction queryFunction = GetQueryFunctionForTabPage(tabControl1.SelectedTab);
            if (queryFunction is QueryFunctionListItem)
            {
                FindText(UIUtilities.GetComboBox(tabControl1.SelectedTab.Controls[0]), tstFindText.Text);
            }
            else if (queryFunction is QueryFunctionGetParametersFromList)
            {
                FindText(UIUtilities.GetListView(tabControl1.SelectedTab.Controls[0]), tstFindText.Text);
            }
            */
        }

        private void FindText(ComboBox comboBox, string textToFind)
        {
            int startIndex = 0;
            if (comboBox.SelectedIndex != -1)
            {
                startIndex = comboBox.SelectedIndex + 1;
            }
            int lineFound = FindText(comboBox, textToFind, startIndex);
            if (lineFound == -1)
            {
                MessageBox.Show("Not found", "Find");
            }
            else
            {
                comboBox.SelectedIndex = lineFound;
            }
        }

        private int FindText(ComboBox comboBox, string text, int startLine)
        {
            int lineFound = -1;

            // Loop through up to twice, first time from startLine to end, the second time from 0 to end
            for (int loop = 0; loop < 2 && lineFound == -1; loop++)
            {
                int currentLine = 0;
                switch (loop)
                {
                    case 0: currentLine = startLine; break;
                    case 1: currentLine = 0; break;     // Start from top                    
                }

                currentLine--;
                while (currentLine < comboBox.Items.Count - 1 && lineFound == -1)
                {
                    currentLine++;
                    string currentText = CFUtilities.ReflectionUtilities.GetMemberValue(comboBox.Items[currentLine], "Name").ToString();
                    if (currentText.ToLower().Contains(text.ToLower()))
                    {
                        lineFound = currentLine;
                    }
                }
            }
            return lineFound;
        }

        private void FindText(ListView listView, string textToFind)
        {
            int startIndex = 0;
            if (listView.SelectedItems.Count > 0)
            {
                startIndex = listView.SelectedItems[0].Index + 1;
                startIndex = (startIndex > listView.Items.Count - 1) ? 0 : startIndex;
            }
            int lineFound = FindText(listView, textToFind, startIndex);

            if (lineFound == -1)
            {
                MessageBox.Show("Not found", "Find");
            }
            else
            {
                listView.HideSelection = false;
                for (int index = 0; index < listView.Items.Count; index++)
                {
                    listView.Items[index].Selected = (index == lineFound) ? true : false;
                }
                listView.TopItem = listView.Items[lineFound];
            }
        }

        private int FindText(ListView listView, string text, int startLine)
        {
            int lineFound = -1;

            // Loop through up to twice, first time from startLine to end, the second time from 0 to end
            for (int loop = 0; loop < 2 && lineFound == -1; loop++)
            {
                int currentLine = 0;
                switch (loop)
                {
                    case 0: currentLine = startLine; break;
                    case 1: currentLine = 0; break;     // Start from top                    
                }

                currentLine--;
                while (currentLine < listView.Items.Count - 1 && lineFound == -1)
                {
                    currentLine++;
                    string currentText = listView.Items[currentLine].Text;    //GetMemberValueObject(currentItem, _displayMember).ToString();
                    if (currentText.ToLower().Contains(text.ToLower()))
                    {
                        lineFound = currentLine;
                    }
                }
            }
            return lineFound;
        }

        private void tstFindText_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindText();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FindText();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            bool isChecked = false;

            switch (btnSelect.Text)
            {
                case "Select All":
                    isChecked = true;
                    btnSelect.Text = "Unselect All";
                    break;
                case "Unselect All":
                    isChecked = false;
                    btnSelect.Text = "Select All";
                    break;
            }

            ListView listView = UIUtilities.GetListView(tabControl1.SelectedTab.Controls[0]);
            if (listView != null)
            {
                for (int index = 0; index < listView.Items.Count; index++)
                {
                    listView.Items[index].Checked = isChecked;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleTabSelected();
        }

        private void HandleTabSelected()
        {
            QueryFunction queryFunction = GetQueryFunctionForTabPage(tabControl1.SelectedTab);
            btnSelect.Enabled = (queryFunction is QueryFunctionListItem);
            if (btnSelect.Enabled)
            {
                ListView listView = UIUtilities.GetListView(tabControl1.SelectedTab.Controls[0]);
                btnSelect.Text = (listView.CheckedItems.Count == 0) ? "Select All" : "Unselect All";
            }
        }
    }
}
