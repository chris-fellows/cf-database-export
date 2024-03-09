//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using CFDatabaseExport.Models;
//using CFDatabaseExport.Utilities;

//namespace CFDatabaseExport.Services
//{
//    /// <summary>
//    /// Configuration data service
//    /// </summary>
//    internal class ConfigurationService
//    {
//        private readonly ApplicationObject _applicationObject;

//        public ConfigurationService(ApplicationObject applicationObject)
//        {
//            _applicationObject = applicationObject;
//        }

//        ///// <summary>
//        ///// Returns all output formats
//        ///// </summary>
//        ///// <param name="dataGridViews"></param>
//        ///// <returns></returns>
//        //public  List<OutputFormat> GetOutputFormats(List<System.Windows.Forms.DataGridView> dataGridViews)
//        //{          
//        //    List<OutputFormat> outputFormats = new List<OutputFormat>();
//        //    QueryOptions queryOptions = null;        

//        //    queryOptions = new QueryOptionsCSV() { Delimiter = ',', DateFormat = _applicationObject.DateTimeFormat1, NullString = "null", FileName = Path.Combine(_applicationObject.OutputFolder, "Results.csv") };
//        //    outputFormats.Add(new OutputFormat("Character delimited file (*.csv | *.txt)", "Exports query results to character separated file", new CFDatabaseExport.Controls.ControlOptionsCSV((QueryOptionsCSV)queryOptions), queryOptions ));

//        //    queryOptions = new QueryOptionsXLS() { FileName = Path.Combine(_applicationObject.OutputFolder, "Results.xlsx") };
//        //    outputFormats.Add(new OutputFormat("Excel file (*.xlsx)", "Exports query results to XSLX file", new CFDatabaseExport.Controls.ControlOptionsXLS((QueryOptionsXLS)queryOptions), queryOptions));

//        //    queryOptions = new QueryOptionsXSLT() { FileName = Path.Combine(_applicationObject.OutputFolder, "Results.htm"), TransformFile = _applicationObject.DefaultXSLTransformFile };
//        //    outputFormats.Add(new OutputFormat("File from XSL transform", "Creates a new file (For example, An HTML file) by applying an XSL transform to the query results", new CFDatabaseExport.Controls.ControlOptionsXSLT((QueryOptionsXSLT)queryOptions), queryOptions));

//        //    queryOptions = new QueryOptionsHTML() { DateFormat = _applicationObject.DateTimeFormat1, NullString = "null", FileName = Path.Combine(_applicationObject.OutputFolder, "Results.htm"), TemplateFile = _applicationObject.DefaultHTMLTemplateFile };
//        //    outputFormats.Add(new OutputFormat("HTML file (*.htm)", "Exports query results to HTML file", new CFDatabaseExport.Controls.ControlOptionsHTML((QueryOptionsHTML)queryOptions), queryOptions));

//        //    queryOptions = new QueryOptionsGrid() { Grids = dataGridViews, DateFormat = _applicationObject.DateTimeFormat1, NullString = "null" };
//        //    outputFormats.Add(new OutputFormat("Screen", "Displays query results on screen", new CFDatabaseExport.Controls.ControlOptionsGrid((QueryOptionsGrid)queryOptions), queryOptions));

//        //    queryOptions = new QueryOptionsJSON() { DateFormat = _applicationObject.DateTimeFormat1, NullString = "null", FileName = Path.Combine(_applicationObject.OutputFolder, "Results.json") };
//        //    outputFormats.Add(new OutputFormat("JSON file (*.json)", "Exports query results to JSON file", new CFDatabaseExport.Controls.ControlOptionsJSON((QueryOptionsJSON)queryOptions), queryOptions));

//        //    queryOptions = new QueryOptionsSQL() { DateFormat = _applicationObject.DateTimeFormat1, FileName = Path.Combine(_applicationObject.OutputFolder, "Results.sql"), TemplateSQLFile = _applicationObject.DefaultTemplateSQLFile, RowTemplateSQL = "INSERT INTO MyNewTable(Column1, Column2) VALUES (##COLUMN_VALUE_1##, ##COLUMN_VALUE_2##)" };
//        //    outputFormats.Add(new OutputFormat("SQL file (*.sql)", "Creates a SQL file by taking a template SQL file and then replacing the ##ROW_TEMPLATES## placeholder with a SQL template for each row returned by the query", new CFDatabaseExport.Controls.ControlOptionsSQL((QueryOptionsSQL)queryOptions), queryOptions));

//        //    queryOptions = new QueryOptionsXML() { FileName = Path.Combine(_applicationObject.OutputFolder, "Results.xml") };
//        //    outputFormats.Add(new OutputFormat("XML file (*.xml)", "Exports query results to XML file", new CFDatabaseExport.Controls.ControlOptionsXML((QueryOptionsXML)queryOptions), queryOptions));      

//        //    return outputFormats;
//        //}

//        ///// <summary>
//        ///// Returns output formats for samples
//        ///// </summary>
//        ///// <param name="dataGridViews"></param>
//        ///// <param name="databaseInfoList"></param>
//        ///// <param name="queryList"></param>
//        ///// <returns></returns>
//        //public List<SampleOutputFormat> GetSampleOutputFormats(List<System.Windows.Forms.DataGridView> dataGridViews,
//        //                                List<DatabaseInfo> databaseInfoList,
//        //                                List<Query> queryList)
//        //{
//        //    List<SampleOutputFormat> outputFormats = new List<SampleOutputFormat>();
//        //    QueryOptions queryOptions = null;
//        //    OutputFormat outputFormat = null;

//        //    DatabaseInfo databaseInfo = databaseInfoList.Find(item => (item.DisplayName.Equals(SampleUtilities.SampleDatabaseDisplay)));
//        //    string queryFile = SampleUtilities.SampleXSLQueryFileName;
//        //    SQLQuery sqlQuery = (SQLQuery)queryList.FirstOrDefault(q => q.GetType() == typeof(SQLQuery));
                
//        //    queryOptions = new QueryOptionsCSV() { Delimiter = ',', DateFormat = _applicationObject.DateTimeFormat1, NullString = "null", FileName = Path.Combine(_applicationObject.OutputFolder, "Results.csv") };
//        //    outputFormat = new OutputFormat("Character delimited file", "CSV file", new CFDatabaseExport.Controls.ControlOptionsCSV((QueryOptionsCSV)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("Character delimited file", databaseInfo, sqlQuery, outputFormat));

//        //    queryOptions = new QueryOptionsXLS() { FileName = Path.Combine(_applicationObject.OutputFolder, "Results.xlsx") };
//        //    outputFormat = new OutputFormat("Excel file (*.xlsx)", "Exports query results to XSLX file", new CFDatabaseExport.Controls.ControlOptionsXLS((QueryOptionsXLS)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("Excel file", databaseInfo,  sqlQuery, outputFormat));

//        //    queryOptions = new QueryOptionsXSLT() { FileName = Path.Combine(_applicationObject.OutputFolder, "Results.htm"), TransformFile = SampleUtilities.SampleXSLTransformFile };
//        //    outputFormat = new OutputFormat("File from XSL transform", "Creates a new file (For example, An HTML file) by applying an XSL transform to the query results", new CFDatabaseExport.Controls.ControlOptionsXSLT((QueryOptionsXSLT)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("XSL transform", databaseInfo, sqlQuery, outputFormat));

//        //    queryOptions = new QueryOptionsHTML() { DateFormat = _applicationObject.DateTimeFormat1, NullString = "null", FileName = Path.Combine(_applicationObject.OutputFolder, "Results.htm"), TemplateFile = _applicationObject.DefaultHTMLTemplateFile };
//        //    outputFormat = new OutputFormat("HTML file (*.htm)", "Exports query results to HTML file", new CFDatabaseExport.Controls.ControlOptionsHTML((QueryOptionsHTML)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("HTML file", databaseInfo, sqlQuery, outputFormat));

//        //    queryOptions = new QueryOptionsJSON() { DateFormat = _applicationObject.DateTimeFormat1, NullString = "null", FileName = Path.Combine(_applicationObject.OutputFolder, "Results.json") };
//        //    outputFormat = new OutputFormat("JSON file", "JSON file", new CFDatabaseExport.Controls.ControlOptionsJSON((QueryOptionsJSON)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("JSON file", databaseInfo, sqlQuery, outputFormat));

//        //    //queryOptions = new QueryOptionsGrid() { Grids = dataGridViews, DateFormat = ApplicationObject.DateTimeFormat1, NullString = "null" };
//        //    //outputFormat = new OutputFormat("Screen", "Displays query results on screen", new CFDatabaseExport.Controls.ControlOptionsGrid((QueryOptionsGrid)queryOptions), queryOptions));
//        //    //outputFormats.Add(new SampleOutputFormat("Screen", sqlQuery, outputFormat));

//        //    queryOptions = new QueryOptionsSQL() { DateFormat = _applicationObject.DateTimeFormat1, FileName = Path.Combine(_applicationObject.OutputFolder, "Results.sql"), TemplateSQLFile = _applicationObject.DefaultTemplateSQLFile, RowTemplateSQL = "INSERT INTO MyNewTable(Column1, Column2) VALUES (##COLUMN_VALUE_1##, ##COLUMN_VALUE_2##)" };
//        //    outputFormat = new OutputFormat("SQL file (*.sql)", "Creates a SQL file by taking a template SQL file and then replacing the ##ROW_TEMPLATES## placeholder with a SQL template for each row returned by the query", new CFDatabaseExport.Controls.ControlOptionsSQL((QueryOptionsSQL)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("SQL file", databaseInfo, sqlQuery, outputFormat));

//        //    queryOptions = new QueryOptionsXML() { FileName = Path.Combine(_applicationObject.OutputFolder, "Results.xml") };
//        //    outputFormat = new OutputFormat("XML file (*.xml)", "Exports query results to XML file", new CFDatabaseExport.Controls.ControlOptionsXML((QueryOptionsXML)queryOptions), queryOptions);
//        //    outputFormats.Add(new SampleOutputFormat("XML file", databaseInfo, sqlQuery, outputFormat));

//        //    return outputFormats;
//        //}      
//    }
//}
