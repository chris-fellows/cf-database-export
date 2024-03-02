using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CFUtilities;
using CFUtilities.XML;
using CFUtilities.Logging;
using CFUtilities.Databases;

namespace CFDatabaseExport
{
    public partial class MainForm : Form, IProgress
    {
        private IQueryRepository _queryRespository = null;
        private ILogWriter _logWriter = null;
        private List<DatabaseInfo> _databaseInfoList = new List<DatabaseInfo>();

        public MainForm()
        {
            InitializeComponent();

            Samples.CreateQueryFunctions();

            //DeveloperUtilities.CreateDatabaseInfos(@"C:\Data\Development\C#\CFDatabaseExport\Data\Database Info");
            //Database1Builder.CreateDatabase1(@"C:\Data\Development\C#\CFDatabaseExport\bin\Debug\Data\Databases\Order Database");
            //DeveloperUtilities.CreateFunctions(@"C:\Data\Database Utilities\Function");

            _queryRespository = Factory.GetDefaultQueryRepository();
            _logWriter = Factory.GetDefaultLogWriter();

            //CreateData();
            InitializeScreen();
            tscbDatabase.SelectedIndex = 0;

            DisplayStatus("Ready");
        }

        public void SetStatusMessage(CFUtilities.Message message)
        {
            
        }

        private void CreateData()
        {                       
            ItemList<DatabaseInfo> databaseInfoList = new ItemList<DatabaseInfo>();

            DatabaseInfo databaseInfo1 = new DatabaseInfo()
            {
                DisplayName = "Local",
                ConnectionString = @"Server=MYMACHINE\SQLEXPRESS;Database=XXX;Trusted_Connection=True;"
            };
            databaseInfoList.Items.Add(databaseInfo1);
            //XmlSerializationUtilities.SerializeToFile<DatabaseInfo>(databaseInfo1, ApplicationObject.DatabaseFolder + @"\Database Info.Local.xml");

            DatabaseInfo databaseInfo2 = new DatabaseInfo()
            {
                DisplayName = "Local",
                ConnectionString = "Server=172.28.124.51;Database=XXX;User Id=sa; Password=XXX;"
            };
            databaseInfoList.Items.Add(databaseInfo2);
            //XmlSerializationUtilities.SerializeToFile<DatabaseInfo>(databaseInfo2, ApplicationObject.DatabaseFolder + @"\Database Info.Production.xml");

            XmlSerialization.SerializeToFile<ItemList<DatabaseInfo>>(databaseInfoList, ApplicationObject.DatabaseInfoFolder + @"\Database Info.xml");
        }

        private void InitializeScreen()
        {
            IDatabaseInfoRepository databaseInfoRepository = new XmlDatabaseInfoRepository(ApplicationObject.DatabaseInfoFolder);
            _databaseInfoList = databaseInfoRepository.GetAll();
            tscbDatabase.Items.Clear();            
            foreach(DatabaseInfo databaseInfo in _databaseInfoList)
            {                
                tscbDatabase.Items.Add(databaseInfo.DisplayName);
            }            

            // Bit of a kludge that we pass this to Factory
            // TO DO: Remove this
            List<DataGridView> dataGridViews = new List<DataGridView>()
            {
                this.dataGridView1, this.dataGridView2, this.dataGridView3, this.dataGridView4, this.dataGridView5 
            };

            // Load output formats, default to display on screen
            List<OutputFormat> outputFormats = Factory.GetOutputFormats(dataGridViews);
            cboOutputFormat.DisplayMember = "Display";
            cboOutputFormat.ValueMember = "Display";
            cboOutputFormat.DataSource = outputFormats;
            OutputFormat selectedOutputFormat = outputFormats.Find(item => item.QueryOptions is QueryOptionsGrid);
            cboOutputFormat.SelectedValue = selectedOutputFormat.Display;

            // Load samples
            List<Query> queryList = _queryRespository.GetAll();
            List<SampleOutputFormat> sampleOutputFormats = Factory.GetSampleOutputFormats(dataGridViews, _databaseInfoList, queryList);          
            tscbSample.ComboBox.DisplayMember = "DisplayName";
            tscbSample.ComboBox.ValueMember = "DisplayName";
            tscbSample.ComboBox.DataSource = sampleOutputFormats;
            SampleOutputFormat selectedSampleOutputFormat = sampleOutputFormats[0];
            tscbSample.ComboBox.SelectedValue = selectedSampleOutputFormat.DisplayName;
      
            // Load queries          
            LoadQueryList(this.tvwQuery, _databaseInfoList[0].ID);
        }

        private DatabaseInfo SelectedDatabaseInfo
        {
            get { return _databaseInfoList[tscbDatabase.SelectedIndex]; }
        }

        private QueryOptions GetQueryOptions()
        {
            OutputFormat outputFormat = (OutputFormat)cboOutputFormat.SelectedItem;
            outputFormat.ApplyUserControlOptionsToModel();
            outputFormat.QueryOptions.ConnectionString = SelectedDatabaseInfo.ConnectionString;
            return outputFormat.QueryOptions;
        }

        private void DisplayOutputOptionsPanel()
        {        
            pnlOutputOptions.Controls.Clear();
            OutputFormat outputFormat = (OutputFormat)cboOutputFormat.SelectedItem;
            txtOutputFormatComments.Text = outputFormat.Comments;
            UserControl control = outputFormat.OptionsUserControl;
            if (control != null)
            {
                pnlOutputOptions.Controls.Add(control);
            }
        }
        
        private void LoadQueryList(TreeView treeView, Guid databaseID)
        {
            treeView.Nodes.Clear();
            TreeNode nodeRoot = treeView.Nodes.Add("Query", "Query");

            List<Query> allQueryList = _queryRespository.GetAll().Where(q => q.DatabaseID == databaseID).ToList();

            //List<SQLQuery> queryList = LoadQueryList(nodeRoot, locationId, allQueryList);
            DisplayQueryList(nodeRoot, allQueryList);
            treeView.Nodes[0].Expand();     
        }

        private void DisplayQueryList(TreeNode nodeParent, List<Query> allQueryList)
        {
            //List<SQLQuery> allQueryList = queryRepository.GetAll(locationId)
        
                // Add queries in this location
                foreach(Query query in allQueryList)
                {
                    TreeNode nodeQuery = nodeParent.Nodes.Add(query.Name, query.Name);
                    nodeQuery.Tag = query;
                }

                // Get child locations
                //List<string> childLocationIds = new List<string>();
                //List<string> childLocationNames = new List<string>();
                //foreach(SQLQuery currentQuery in allQueryList)
                //{
                //    if (currentQuery.ParentLocationID.Equals(locationId, StringComparison.InvariantCultureIgnoreCase))
                //    {
                //        if (!childLocationIds.Contains(currentQuery.ParentLocationID))
                //        {
                //            childLocationIds.Add(currentQuery.ParentLocationID);
                //            childLocationNames.Add(currentQuery.ParentLocationName);
                //        }                    
                //    }
                //}

                //for (int index = 0; index < childLocationIds.Count; index++)
                //{
                //    TreeNode nodeChild = nodeParent.Nodes.Add(childLocationNames[index], childLocationNames[index]);
                //    DisplayQueryList(nodeChild, childLocationIds[index], allQueryList);
                //}
                
                      
            // Load queries in this folder
            /*
            foreach (string file in Directory.GetFiles(folder, "*.sql"))
            {
                TreeNode nodeQuery = null;
                string queryName = Path.GetFileNameWithoutExtension(file);
                if (nodeParent != null)
                {
                    nodeQuery = nodeParent.Nodes.Add(queryName, queryName);
                }
                SQLQuery sqlQuery = new SQLQuery() { Name = queryName, ID = file, HasResultset = true };
                if (nodeParent != null)
                {
                    nodeQuery.Tag = sqlQuery;
                }
                queryList.Add(sqlQuery);
            }
            */

            // Check sub-folders
            /*
            foreach (string subFolder in Directory.GetDirectories(folder))
            {
                string folderName = new DirectoryInfo(subFolder).Name;
                TreeNode nodeFolder = nodeParent.Nodes.Add(folderName, folderName);

                List<SQLQuery> subFolderQueryList = LoadQueryList(nodeFolder, subFolder);
                subFolderQueryList.ForEach(item => queryList.Add(item));

                // Load queries
                /*
                foreach (string file in Directory.GetFiles(subFolder, "*.sql"))
                {
                    string queryName = Path.GetFileNameWithoutExtension(file);
                    TreeNode nodeQuery = nodeFolder.Nodes.Add(queryName, queryName);
                    SQLQuery sqlQuery = new SQLQuery() { FileName = file, HasResultset = true };
                    nodeQuery.Tag = sqlQuery;
                }

                // Load queries in sub-folders
                foreach(string subSubFolder in Directory.GetDirectories(subFolder))
                {
                    string subSubFolderName = Path.GetDirectoryName(subSubFolder);
                    TreeNode nodeSubSubFolder = nodeParent.Nodes.Add(subSubFolderName, subSubFolderName);
                    LoadQueryList(nodeSubSubFolder, subSubFolder);
                }
                */
            //}
            
            //return queryList;
        }

        private void tvwQuery_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is SQLQuery)
            {
                SQLQuery sqlQuery = (SQLQuery)e.Node.Tag;
                HandleQuerySelected(sqlQuery);
            }
        }
        
        private void HandleQuerySelected(SQLQuery sqlQuery)
        {

        }

        private SQLQuery SelectedSqlQuery
        {
            get
            {
                if (tvwQuery.SelectedNode != null && tvwQuery.SelectedNode.Tag != null)
                {
                    return (SQLQuery)tvwQuery.SelectedNode.Tag;
                }
                return null;
            }
        }
                
        //private void btnRun_Click(object sender, EventArgs e)
        //{            
        //    SQLQuery query = SelectedSqlQuery;
        //    IQueryRepository queryRepository = _queryRespository;

        //    OutputFormat outputFormat = (OutputFormat)cboOutputFormat.SelectedItem;
        //    if (outputFormat.CanApplyUserControlOptionsToModel())
        //    {
        //        QueryOptions queryOptions = GetQueryOptions();
                              
        //        IQueryHandler queryHandler = Factory.GetQueryHandlers().Find(item => (item.Supports(queryOptions)));
        //        QueryManager queryManager = new QueryManager();
        //        if (queryHandler.VisibleOutput)
        //        {
        //            tabControl1.SelectedTab = tabPage2; // Select Results
        //        }
        //        queryManager.RunQuery(query, queryOptions, queryHandler, queryRepository, this);

        //        // Open output folder
        //        IOUtilities.OpenDirectoryWithExplorer(ApplicationObject.OutputFolder);

        //        MessageBox.Show("Query run", "Run");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Cannot run the query because the options are invalid", "Error");
        //    }
        //}

        private void cboOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayOutputOptionsPanel();
        }

        private void tscbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {         
            LoadQueryList(this.tvwQuery, SelectedDatabaseInfo.ID);
        }

        private void tsbRun_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

        private void DisplayStatus(string status)
        {
            tssStatus.Text = string.Format(" {0}", status).Trim();            
        }

        private void RunQuery()
        {                
            SQLQuery query = SelectedSqlQuery;
            IQueryRepository queryRepository = _queryRespository;           

            OutputFormat outputFormat = (OutputFormat)cboOutputFormat.SelectedItem;
            if (outputFormat.CanApplyUserControlOptionsToModel())
            {
                DisplayStatus("Running query...");

                QueryOptions queryOptions = GetQueryOptions();

                // Get SQL generator
                ISQLGenerator sqlGenerator = Factory.GetSQLGenerations().FirstOrDefault(sg => sg.GetType().Name.Contains(SelectedDatabaseInfo.SQLGenerator));

                IQueryFunctionRepository queryFunctionRepository = new XmlQueryFunctionRepository(ApplicationObject.QueryFunctionFolder);

                IQueryHandler queryHandler = Factory.GetQueryHandlers().Find(item => (item.Supports(queryOptions)));
                IQueryExecutor queryExecutor = new SQLQueryExecutor();                
                if (queryHandler.VisibleOutput)
                {
                    tabControl1.SelectedTab = tabPage2; // Select Results
                }
                queryExecutor.ExecuteQuery(query, queryOptions, queryHandler, queryRepository, queryFunctionRepository, sqlGenerator, this);

                // Open output folder, not necessary for grid output
                if (outputFormat.QueryOptions as QueryOptionsGrid == null)
                {
                    IOUtilities.OpenDirectoryWithExplorer(ApplicationObject.OutputFolder);
                }

                DisplayStatus("Ready");
                //MessageBox.Show("Query run", "Run");
            }
            else
            {
                MessageBox.Show("Cannot run the query because the options are invalid", "Error");
            }
        }

        private void RunSampleQuery()
        {
            //SQLQuery query = SelectedSqlQuery;
            IQueryRepository queryRepository = _queryRespository;

            SampleOutputFormat sampleOutputFormat = (SampleOutputFormat)tscbSample.ComboBox.SelectedItem;
            //OutputFormat outputFormat = (OutputFormat)cboOutputFormat.SelectedItem;
            //if (outputFormat.CanApplyUserControlOptionsToModel())
            //{
            QueryOptions queryOptions = sampleOutputFormat.OutputFormat.QueryOptions;
            queryOptions.ConnectionString = sampleOutputFormat.DatabaseInfo.ConnectionString;

            // Get SQL generator
            ISQLGenerator sqlGenerator = Factory.GetSQLGenerations().FirstOrDefault(sg => sg.GetType().Name.Contains(SelectedDatabaseInfo.SQLGenerator));


            IQueryHandler queryHandler = Factory.GetQueryHandlers().Find(item => (item.Supports(queryOptions)));
                IQueryExecutor queryExecutor = new SQLQueryExecutor();
                if (queryHandler.VisibleOutput)
                {
                    tabControl1.SelectedTab = tabPage2; // Select Results
                }
                queryExecutor.ExecuteQuery(sampleOutputFormat.SQLQuery, queryOptions, queryHandler, queryRepository, null, sqlGenerator, this);

                // Open output folder
                IOUtilities.OpenDirectoryWithExplorer(ApplicationObject.OutputFolder);

                MessageBox.Show("Query run", "Run");
            //}
            //else
            //{
            //    MessageBox.Show("Cannot run the query because the options are invalid", "Error");
            //}
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tsbRunSample_Click(object sender, EventArgs e)
        {
            RunSampleQuery();
        }
    }
}
