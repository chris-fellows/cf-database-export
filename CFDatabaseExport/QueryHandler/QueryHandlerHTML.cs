using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CFDatabaseExport
{
    /// <summary>
    /// Handles output of query to HTML file
    /// </summary>
    public class QueryHandlerHTML : IQueryHandler
    {
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsHTML queryOptions = (QueryOptionsHTML)queryOptionsX;

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

                // Set style
                string styleHtml = "<style>" +
                                   ".body " +
                                   "{ " +
                                   "} " +
                                   ".table " +
                                   "{ " +
                                        "border: 1px solid black; " +
                                   "} " +
                                   ".td " +
                                   "{ " +
                                        "border: 1px solid black; " +
                                   "} " +                    
                                   ".th" +
                                   "{ " +
                                        "border: 1px solid black; " +
                                   "} " +
                                   ".tr " +
                                   "{ " +
                                   "} " +
                                   "</style>";

                string templateHtml = "<html><head>" + styleHtml + "</head><body class='body'>{table}</body></html>";
                if (!String.IsNullOrEmpty(queryOptions.TemplateFile))
                {
                    templateHtml = File.ReadAllText(queryOptions.TemplateFile);
                }

                StringBuilder tableHtml = new StringBuilder("<table class='table'>");
                
                // Write headers
                for (int field = 0; field < dataTable.Columns.Count; field++)
                {
                    tableHtml.Append(string.Format("<th class='th'>{0}</th>", dataTable.Columns[field].ColumnName));
                }

                // Write rows
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    string rowHtml = GetTableRow(queryOptions, dataTable, columnFormats, rowIndex);
                    tableHtml.Append(rowHtml);
                }
                tableHtml.Append("</table>");

                // Set table in template
                string html = templateHtml.Replace("{table}", tableHtml.ToString());

                // Save
                File.WriteAllText(filename, html);
            }
        }


        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsHTML);
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
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsHTML)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsHTML queryOptions, DataTable dataTable, int column)
        {
            ColumnFormat columnFormat = new ColumnFormat();
            try
            {
                //Controls.ControlOptionsHTML controlOptions = (Controls.ControlOptionsHTML)queryOptions.OptionsUserControl;
                String defaultQuoted = "'";
                columnFormat.ColumnName = dataTable.Columns[column].ColumnName;
                columnFormat.MapConverter.AddMapping("", "&nbsp;");
                columnFormat.MapConverter.AddMapping(null, queryOptions.NullString);
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
                        columnFormat.FormatConverter.SetFormat(queryOptions.DateFormat);
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

        private string GetTableRow(QueryOptionsHTML queryOptions, DataTable dataTable, ColumnFormats columnFormats, int rowIndex)
        {
            StringBuilder html = new StringBuilder("<tr class='tr'>");            
            for (int field = 0; field < dataTable.Columns.Count; field++)
            {
                ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[field].ColumnName);
                string fieldValue = columnFormat.FormatValue(dataTable.Rows[rowIndex][field]);
                //string fieldValue = GetFieldValue(dataTable, rowIndex, field, queryOptions.NullValueString, queryOptions.DateFormat, false, ApplicationObject.Quote);
                //if (String.IsNullOrEmpty(fieldValue))
                //{
                //    fieldValue = "&nbsp;";
                //}
                html.Append(string.Format("<td class='td'>{0}</td>", fieldValue));
            }
            html.Append("</tr>");
            return html.ToString();
        }        
    }
}
