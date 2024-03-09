using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CFDatabaseExport.Models;
using CFDatabaseExport.Utilities;

namespace CFDatabaseExport
{
    /// <summary>
    /// Application configuration
    /// </summary>
    public class ApplicationObject
    {
        private readonly string _rootFolder;

        public ApplicationObject(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        public string DateTimeFormat1
        {
            get
            {
                return "dd-MM-yyyy HH:mm:ss FFFF";
            }
        } 
        //public static Char Quote = '\'';
      
        public string DatabaseInfoFolder
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Database Info");
            }
        }

        public string DatabaseTypeFolder
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Database Types");                
            }
        }

        public string QueryFunctionFolder
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Query Functions");                
            }
        }

        public string QueryFolder
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Queries");                
            }
        }

        public string OutputFolder
        {
            get
            {                
                return Path.Combine(_rootFolder, "Data", "Output");
            }
        }

        public string DefaultHTMLTemplateFile
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Templates", "HTML", "Template 1.htm");
                //return string.Format(@"{0}\Data\Templates\HTML\Template 1.htm", Environment.CurrentDirectory);
            }
        }

        public string DefaultXSLTransformFile
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Templates", "XSLT", "Customer list.xslt");
                //return string.Format(@"{0}\Data\Templates\XSLT\Customer list.xslt", Environment.CurrentDirectory);               
            }
        }

        public string DefaultTemplateSQLFile
        {
            get
            {
                return Path.Combine(_rootFolder, "Data", "Templates", "SQL", "Customer list.sql");
                //return string.Format(@"{0}\Data\Templates\SQL\Customer list.sql", Environment.CurrentDirectory);
            }
        }       

        public string GetQueriesLocationID(DatabaseInfo databaseInfo)
        {
            return Path.Combine(_rootFolder, "Data", "Queries", databaseInfo.DisplayName);
            //return string.Format(@"{0}\Data\Queries\{1}", Environment.CurrentDirectory, databaseInfo.DisplayName);           
        }

        /// <summary>
        /// Returns all output formats
        /// </summary>
        /// <param name="dataGridViews"></param>
        /// <returns></returns>
        public List<OutputFormat> GetOutputFormats(List<System.Windows.Forms.DataGridView> dataGridViews)
        {
            List<OutputFormat> outputFormats = new List<OutputFormat>();
            QueryOptions queryOptions = null;

            queryOptions = new QueryOptionsCSV() { Delimiter = ',', DateFormat = this.DateTimeFormat1, NullString = "null", FileName = Path.Combine(this.OutputFolder, "Results.csv") };
            outputFormats.Add(new OutputFormat("Character delimited file (*.csv | *.txt)", "Exports query results to character separated file", new CFDatabaseExport.Controls.ControlOptionsCSV((QueryOptionsCSV)queryOptions), queryOptions));

            queryOptions = new QueryOptionsXLS() { FileName = Path.Combine(this.OutputFolder, "Results.xlsx") };
            outputFormats.Add(new OutputFormat("Excel file (*.xlsx)", "Exports query results to XSLX file", new CFDatabaseExport.Controls.ControlOptionsXLS((QueryOptionsXLS)queryOptions), queryOptions));

            queryOptions = new QueryOptionsXSLT() { FileName = Path.Combine(this.OutputFolder, "Results.htm"), TransformFile = this.DefaultXSLTransformFile };
            outputFormats.Add(new OutputFormat("File from XSL transform", "Creates a new file (For example, An HTML file) by applying an XSL transform to the query results", new CFDatabaseExport.Controls.ControlOptionsXSLT((QueryOptionsXSLT)queryOptions), queryOptions));

            queryOptions = new QueryOptionsHTML() { DateFormat = this.DateTimeFormat1, NullString = "null", FileName = Path.Combine(this.OutputFolder, "Results.htm"), TemplateFile = this.DefaultHTMLTemplateFile };
            outputFormats.Add(new OutputFormat("HTML file (*.htm)", "Exports query results to HTML file", new CFDatabaseExport.Controls.ControlOptionsHTML((QueryOptionsHTML)queryOptions), queryOptions));

            queryOptions = new QueryOptionsGrid() { Grids = dataGridViews, DateFormat = this.DateTimeFormat1, NullString = "null" };
            outputFormats.Add(new OutputFormat("Screen", "Displays query results on screen", new CFDatabaseExport.Controls.ControlOptionsGrid((QueryOptionsGrid)queryOptions), queryOptions));

            queryOptions = new QueryOptionsJSON() { DateFormat = this.DateTimeFormat1, NullString = "null", FileName = Path.Combine(this.OutputFolder, "Results.json") };
            outputFormats.Add(new OutputFormat("JSON file (*.json)", "Exports query results to JSON file", new CFDatabaseExport.Controls.ControlOptionsJSON((QueryOptionsJSON)queryOptions), queryOptions));

            queryOptions = new QueryOptionsSQL() { DateFormat = this.DateTimeFormat1, FileName = Path.Combine(this.OutputFolder, "Results.sql"), TemplateSQLFile = this.DefaultTemplateSQLFile, RowTemplateSQL = "INSERT INTO MyNewTable(Column1, Column2) VALUES (##COLUMN_VALUE_1##, ##COLUMN_VALUE_2##)" };
            outputFormats.Add(new OutputFormat("SQL file (*.sql)", "Creates a SQL file by taking a template SQL file and then replacing the ##ROW_TEMPLATES## placeholder with a SQL template for each row returned by the query", new CFDatabaseExport.Controls.ControlOptionsSQL((QueryOptionsSQL)queryOptions), queryOptions));

            queryOptions = new QueryOptionsXML() { FileName = Path.Combine(this.OutputFolder, "Results.xml") };
            outputFormats.Add(new OutputFormat("XML file (*.xml)", "Exports query results to XML file", new CFDatabaseExport.Controls.ControlOptionsXML((QueryOptionsXML)queryOptions), queryOptions));

            return outputFormats;
        }

        /// <summary>
        /// Returns output formats for samples
        /// </summary>
        /// <param name="dataGridViews"></param>
        /// <param name="databaseInfoList"></param>
        /// <param name="queryList"></param>
        /// <returns></returns>
        public List<SampleOutputFormat> GetSampleOutputFormats(//List<System.Windows.Forms.DataGridView> dataGridViews,
                                        List<DatabaseInfo> databaseInfoList,
                                        List<Query> queryList)
        {
            List<SampleOutputFormat> outputFormats = new List<SampleOutputFormat>();
            QueryOptions queryOptions = null;
            OutputFormat outputFormat = null;

            DatabaseInfo databaseInfo = databaseInfoList.Find(item => (item.DisplayName.Equals(SampleUtilities.SampleDatabaseDisplay)));
            string queryFile = SampleUtilities.SampleXSLQueryFileName;
            SQLQuery sqlQuery = (SQLQuery)queryList.FirstOrDefault(q => q.GetType() == typeof(SQLQuery));

            queryOptions = new QueryOptionsCSV() { Delimiter = ',', DateFormat = this.DateTimeFormat1, NullString = "null", FileName = Path.Combine(this.OutputFolder, "Results.csv") };
            outputFormat = new OutputFormat("Character delimited file", "CSV file", new CFDatabaseExport.Controls.ControlOptionsCSV((QueryOptionsCSV)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("Character delimited file", databaseInfo, sqlQuery, outputFormat));

            queryOptions = new QueryOptionsXLS() { FileName = Path.Combine(this.OutputFolder, "Results.xlsx") };
            outputFormat = new OutputFormat("Excel file (*.xlsx)", "Exports query results to XSLX file", new CFDatabaseExport.Controls.ControlOptionsXLS((QueryOptionsXLS)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("Excel file", databaseInfo, sqlQuery, outputFormat));

            queryOptions = new QueryOptionsXSLT() { FileName = Path.Combine(this.OutputFolder, "Results.htm"), TransformFile = SampleUtilities.SampleXSLTransformFile };
            outputFormat = new OutputFormat("File from XSL transform", "Creates a new file (For example, An HTML file) by applying an XSL transform to the query results", new CFDatabaseExport.Controls.ControlOptionsXSLT((QueryOptionsXSLT)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("XSL transform", databaseInfo, sqlQuery, outputFormat));

            queryOptions = new QueryOptionsHTML() { DateFormat = this.DateTimeFormat1, NullString = "null", FileName = Path.Combine(this.OutputFolder, "Results.htm"), TemplateFile = this.DefaultHTMLTemplateFile };
            outputFormat = new OutputFormat("HTML file (*.htm)", "Exports query results to HTML file", new CFDatabaseExport.Controls.ControlOptionsHTML((QueryOptionsHTML)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("HTML file", databaseInfo, sqlQuery, outputFormat));

            queryOptions = new QueryOptionsJSON() { DateFormat = this.DateTimeFormat1, NullString = "null", FileName = Path.Combine(this.OutputFolder, "Results.json") };
            outputFormat = new OutputFormat("JSON file", "JSON file", new CFDatabaseExport.Controls.ControlOptionsJSON((QueryOptionsJSON)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("JSON file", databaseInfo, sqlQuery, outputFormat));

            //queryOptions = new QueryOptionsGrid() { Grids = dataGridViews, DateFormat = ApplicationObject.DateTimeFormat1, NullString = "null" };
            //outputFormat = new OutputFormat("Screen", "Displays query results on screen", new CFDatabaseExport.Controls.ControlOptionsGrid((QueryOptionsGrid)queryOptions), queryOptions));
            //outputFormats.Add(new SampleOutputFormat("Screen", sqlQuery, outputFormat));

            queryOptions = new QueryOptionsSQL() { DateFormat = this.DateTimeFormat1, FileName = Path.Combine(this.OutputFolder, "Results.sql"), TemplateSQLFile = this.DefaultTemplateSQLFile, RowTemplateSQL = "INSERT INTO MyNewTable(Column1, Column2) VALUES (##COLUMN_VALUE_1##, ##COLUMN_VALUE_2##)" };
            outputFormat = new OutputFormat("SQL file (*.sql)", "Creates a SQL file by taking a template SQL file and then replacing the ##ROW_TEMPLATES## placeholder with a SQL template for each row returned by the query", new CFDatabaseExport.Controls.ControlOptionsSQL((QueryOptionsSQL)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("SQL file", databaseInfo, sqlQuery, outputFormat));

            queryOptions = new QueryOptionsXML() { FileName = Path.Combine(this.OutputFolder, "Results.xml") };
            outputFormat = new OutputFormat("XML file (*.xml)", "Exports query results to XML file", new CFDatabaseExport.Controls.ControlOptionsXML((QueryOptionsXML)queryOptions), queryOptions);
            outputFormats.Add(new SampleOutputFormat("XML file", databaseInfo, sqlQuery, outputFormat));

            return outputFormats;
        }
    }
}
