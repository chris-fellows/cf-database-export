using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CFDatabaseExport.Models;
using CFDatabaseExport.QueryHandlers;
//using CFDatabaseExport.Services;
using CFUtilities;
using CFUtilities.XML;
using CFUtilities.Logging;
using CFUtilities.Databases;
using CFDatabaseExport.Exceptions;
using System.Threading;

namespace CFDatabaseExport.Forms
{
    /// <summary>
    /// Main form. Lists all databases, displays queries for selected database, allows users to select query,
    /// select output format and export.
    /// </summary>
    public partial class MainForm : Form, IProgress
    {
        private readonly ApplicationObject _applicationObject;        
        private readonly IDatabaseInfoService _databaseInfoService;
        private readonly IDatabaseTypeService _databaseTypeService;
        private readonly IQueryFunctionService _queryFunctionService = null;
        private readonly IQueryService _queryRespository = null;        
        private readonly ILogWriter _logWriter = null;
        private readonly IEnumerable<IQueryHandler> _queryHandlers;
        private List<DatabaseInfo> _databaseInfoList = new List<DatabaseInfo>();
        private readonly IEnumerable<ISQLGenerator> _sqlGenerators = null;
        private CancellationTokenSource _queryCancelTokenSource = null;

        public MainForm(ApplicationObject applicationObject,
                    IDatabaseInfoService databaseInfoService,
                    IDatabaseTypeService databaseTypeService,
                    ILogWriter logWriter,
                    IQueryFunctionService queryFunctionService,                                        
                    IEnumerable<IQueryHandler> queryHandlers, 
                    IQueryService queryService,
                    IEnumerable<ISQLGenerator> sqlGenerators) 
        {                        
            InitializeComponent();

            //SampleUtilities.CreateQueryFunctions(applicationObject);

            //DeveloperUtilities.CreateDatabaseInfos(@"C:\Data\Development\C#\CFDatabaseExport\Data\Database Info");
            //Database1Builder.CreateDatabase1(@"C:\Data\Development\C#\CFDatabaseExport\bin\Debug\Data\Databases\Order Database");
            //DeveloperUtilities.CreateFunctions(@"C:\Data\Database Utilities\Function");

            //_queryRespository = _configurationService.GetDefaultQueryRepository();
            //_logWriter = _configurationService.GetDefaultLogWriter();            
            _applicationObject = applicationObject;            
            _databaseInfoService = databaseInfoService;
            _databaseTypeService = databaseTypeService;
            _logWriter = logWriter;
            _queryFunctionService = queryFunctionService;
            _queryHandlers = queryHandlers;           
            _queryRespository = queryService;
            _sqlGenerators = sqlGenerators;

            //CreateData();
            InitialiseView();
            tscbDatabase.SelectedIndex = 0;

            DisplayStatus("Ready");
        }

        /// <summary>
        /// Set view for query active
        /// </summary>
        private void SetViewQueryActive()
        {
            tsbRun.Text = "Cancel";           
            toolStripProgressBar1.Value = 0;
        }

        /// <summary>
        /// Set vew for query not active
        /// </summary>
        private void SetViewQueryInactive()
        {
            tsbRun.Text = "Run query";            
            toolStripProgressBar1.Value = 0;
        }

        /// <summary>
        /// Handles progress status message when running query
        /// </summary>
        /// <param name="message"></param>
        void IProgress.SetStatusMessage(CFUtilities.Message message)
        {
            // Not currently set
        }

        /// <summary>
        /// Handles items done when running query
        /// </summary>
        /// <param name="itemsDone"></param>
        /// <param name="itemsTotal"></param>
        void IProgress.SetItemsDone(int itemsDone, int itemsTotal)
        {          
            if (itemsTotal > 0) toolStripProgressBar1.Value = (int)((itemsDone / itemsTotal) * 100);            
        }

        //private void CreateData()
        //{                       
        //    ItemList<DatabaseInfo> databaseInfoList = new ItemList<DatabaseInfo>();

        //    DatabaseInfo databaseInfo1 = new DatabaseInfo()
        //    {
        //        DisplayName = "Local",
        //        ConnectionString = @"Server=MYMACHINE\SQLEXPRESS;Database=XXX;Trusted_Connection=True;"
        //    };
        //    databaseInfoList.Items.Add(databaseInfo1);
        //    //XmlSerializationUtilities.SerializeToFile<DatabaseInfo>(databaseInfo1, ApplicationObject.DatabaseFolder + @"\Database Info.Local.xml");

        //    DatabaseInfo databaseInfo2 = new DatabaseInfo()
        //    {
        //        DisplayName = "Local",
        //        ConnectionString = "Server=172.28.124.51;Database=XXX;User Id=sa; Password=XXX;"
        //    };
        //    databaseInfoList.Items.Add(databaseInfo2);
        //    //XmlSerializationUtilities.SerializeToFile<DatabaseInfo>(databaseInfo2, ApplicationObject.DatabaseFolder + @"\Database Info.Production.xml");

        //    XmlSerialization.SerializeToFile<ItemList<DatabaseInfo>>(databaseInfoList, System.IO.Path.Combine(_applicationObject.DatabaseInfoFolder, "Database Info.xml"));
        //}

        /// <summary>
        /// Initialises view
        /// </summary>
        private void InitialiseView()
        {            
            _databaseInfoList = _databaseInfoService.GetAll();
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
            List<OutputFormat> outputFormats = _applicationObject.GetOutputFormats(dataGridViews);
            cboOutputFormat.DisplayMember = "Display";
            cboOutputFormat.ValueMember = "Display";
            cboOutputFormat.DataSource = outputFormats;
            OutputFormat selectedOutputFormat = outputFormats.Find(item => item.QueryOptions is QueryOptionsGrid);
            cboOutputFormat.SelectedValue = selectedOutputFormat.Display;

            // Load samples
            List<Query> queryList = _queryRespository.GetAll();
            List<SampleOutputFormat> sampleOutputFormats = _applicationObject.GetSampleOutputFormats(_databaseInfoList, queryList);          
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
            switch (tsbRun.Text)
            {
                case "Run query":
                    try
                    {
                        RunQuery();
                        //var task = RunQueryAsync();
                    }
                    catch (HandleOptionsInvalidException)
                    {
                        MessageBox.Show("The options are invalid");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Error running the query:\n{exception.Message}", "Error");
                    }
                    finally
                    {
                        SetViewQueryInactive();
                        DisplayStatus("Ready");
                    }
                    break;
                case "Cancel":          
                    _queryCancelTokenSource.Cancel();
                    break;
            }
        }               

        private void DisplayStatus(string status)
        {
            tssStatus.Text = string.Format(" {0}", status).Trim();            
        }
        
        /// <summary>
        /// Runs action in main thread. This is typically for actions that interact with the UI.
        /// </summary>
        /// <param name="action"></param>
        private void RunInMainThread(Action action)
        {
            this.Invoke((Action)delegate
            {
                action();
            });
        }

        /// <summary>
        /// Runs query and handles the output. Either generates output files (CSV, HTML etc) or displays output to the
        /// screen.
        /// 
        /// TODO: Change this to run in a background thread. We need to change the handling to the screen to update in
        /// the main thread.
        /// </summary>
        private void RunQuery()
        {                
            var query = SelectedSqlQuery;          
            OutputFormat outputFormat = (OutputFormat)cboOutputFormat.SelectedItem;
            var optionsControlMessages = outputFormat.ValidateControlOptionsModel();
            if (!optionsControlMessages.Any())     // Query options valid
            {
                _queryCancelTokenSource = new CancellationTokenSource();

                SetViewQueryActive();

                RunInMainThread(() => DisplayStatus("Running query..."));   // For multi-threading              

                QueryOptions queryOptions = GetQueryOptions();

                // Get SQL generator
                var sqlGenerator = _sqlGenerators.FirstOrDefault(sg => sg.GetType().Name.Contains(SelectedDatabaseInfo.SQLGenerator));
            
                var queryHandler = _queryHandlers.ToList().Find(item => (item.Supports(queryOptions)));
                var queryExecutor = new SQLQueryExecutor();                
                if (queryHandler.VisibleOutput)
                {
                    tabControl1.SelectedTab = tabPage2; // Select Results
                }
                queryExecutor.ExecuteQuery(query, queryOptions, queryHandler, _queryRespository, _queryFunctionService,
                                sqlGenerator, this, _queryCancelTokenSource.Token);
                
                // Open output folder, not necessary for grid output
                if (outputFormat.QueryOptions as QueryOptionsGrid == null)
                {
                    IOUtilities.OpenDirectoryWithExplorer(_applicationObject.OutputFolder);
                }

                RunInMainThread(() => DisplayStatus("Ready"));        // For multi-threading         
            }
            else    // Query options invalid (E.g. Template file does not exist)
            {
                //throw new HandleOptionsInvalidException();
                RunInMainThread(() => MessageBox.Show($"The options are invalid:\n{optionsControlMessages[0]}", "Error"));
            }
        }

        /// <summary>
        /// Runs sample query for demo
        /// </summary>
        private void RunSampleQuery()
        {
            var tokenSource = new CancellationTokenSource();

            //SQLQuery query = SelectedSqlQuery;
            IQueryService queryRepository = _queryRespository;

            SampleOutputFormat sampleOutputFormat = (SampleOutputFormat)tscbSample.ComboBox.SelectedItem;           
            QueryOptions queryOptions = sampleOutputFormat.OutputFormat.QueryOptions;
            queryOptions.ConnectionString = sampleOutputFormat.DatabaseInfo.ConnectionString;

            // Get SQL generator
            var sqlGenerator = _sqlGenerators.FirstOrDefault(sg => sg.GetType().Name.Contains(SelectedDatabaseInfo.SQLGenerator));

            var queryHandler = _queryHandlers.ToList().Find(item => (item.Supports(queryOptions)));
            IQueryExecutor queryExecutor = new SQLQueryExecutor();
            if (queryHandler.VisibleOutput)
            {
                tabControl1.SelectedTab = tabPage2; // Select Results
            }
            queryExecutor.ExecuteQuery(sampleOutputFormat.SQLQuery, queryOptions, queryHandler, queryRepository, 
                            null, sqlGenerator, this, tokenSource.Token);

            // Open output folder
            IOUtilities.OpenDirectoryWithExplorer(_applicationObject.OutputFolder);

            MessageBox.Show("Query run", "Run");        
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tsbRunSample_Click(object sender, EventArgs e)
        {
            try
            {
                RunSampleQuery();
            }
            catch (HandleOptionsInvalidException)
            {
                MessageBox.Show("The options are invalid");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error running the sample query:\n{exception.Message}", "Error");
            }
            finally
            {
                DisplayStatus("Ready");
            }            
        }
    }
}
