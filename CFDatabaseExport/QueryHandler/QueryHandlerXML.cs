using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml;
using CFUtilities.XML;

namespace CFDatabaseExport
{
    /// <summary>
    /// Handles output of query to XML file
    /// </summary>
    public class QueryHandlerXML : IQueryHandler
    {
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsXML queryOptions = (QueryOptionsXML)queryOptionsX;

            int recordsetCount = 0;
            for (int dataTableIndex = 0; dataTableIndex < dataTables.Count; dataTableIndex++)
            {
                DataTable dataTable = dataTables[dataTableIndex];             
                ColumnFormats columnFormats = queryOptions.ColumnFormatsList[dataTableIndex];
                recordsetCount++;
                string filename = queryOptions.FileName;
                filename = filename.Replace("{sequence}", recordsetCount.ToString());
                string folder = Path.GetDirectoryName(filename);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                // If multiple recordsets but there's no {sequence} placeholder then ensure unique filename set
                if (recordsetCount > 1 && !queryOptions.FileName.Contains("{sequence}"))
                {
                    filename = string.Format(@"{0}\{1}.{2}{3}", Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), recordsetCount, Path.GetExtension(filename));
                }

                //DataSet dataSet = new DataSet();
                //dataSet.Tables.Add(dataTable);

                XmlDocument document = new XmlDocument();
                document.LoadXml("<Data></Data>");
                XmlNode nodeData = document.DocumentElement;

                // Create an XML declaration. 
                XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", null, null);
                xmlDeclaration.Encoding = "UTF-8";
                xmlDeclaration.Standalone = "yes";
                document.InsertBefore(xmlDeclaration, nodeData);

                // Export rows
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    XmlNode nodeRow = XmlUtilities.AddChildNodeElement(document, nodeData, "Row", "");
                    for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                    {
                        ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[columnIndex].ColumnName);
                        string fieldValue = columnFormat.FormatValue(dataTable.Rows[rowIndex][columnIndex]);

                        string columnName = dataTable.Columns[columnIndex].ColumnName;
                        XmlNode nodeColumn = XmlUtilities.AddChildNodeElement(document, nodeRow, columnName, fieldValue);
                        //XmlUtilities.AddNodeAttribute(nodeRow, columnName, fieldValue);
                    }
                }
                document.Save(filename);

                /*                
                using (StreamWriter fs = new StreamWriter(filename))
                {
                    dataSet.WriteXml(fs);
                } 
                */
            }
        }

        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsXML);
        }

        public bool VisibleOutput
        {
            get { return false; }
        }

        private void SetColumnFormats(QueryOptions queryOptions, List<DataTable> dataTables)
        {
            queryOptions.ColumnFormatsList.Clear();
            foreach (DataTable dataTable in dataTables)
            {
                ColumnFormats columnFormats = new ColumnFormats();
                for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsXML)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsXML queryOptions, DataTable dataTable, int column)
        {
            ColumnFormat columnFormat = new ColumnFormat();
            try
            {
                String defaultQuoted = "'";
                columnFormat.ColumnName = dataTable.Columns[column].ColumnName;         
                columnFormat.MapConverter.AddMapping(null, "NULL");                
                string columnType = dataTable.Columns[column].DataType.ToString();              
               
                switch (columnType)
                {
                    case "System.Guid":
                        break;
                    case "System.Boolean":
                        columnFormat.MapConverter.AddMapping(true, "Y");
                        columnFormat.MapConverter.AddMapping(false, "N");
                        break;
                    case "System.Byte":
                        break;
                    case "System.Char":
                        break;                    
                    case "System.DateTime":
                        columnFormat.FormatConverter.SetFormat("yyyy-MM-dd HH:mm:ss.FFFFFFF");
                        break;       
                    case "System.Double":
                        break;
                    case "System.Single":
                        break;
                    case "System.String":
                        break;
                    case "System.Int16":
                        break;
                    case "System.Int32":
                        break;                   
                    case "System.Int64":
                        break;
                }
            }
            catch (Exception e)
            {

            }
            return columnFormat;
        }       
    }
}
