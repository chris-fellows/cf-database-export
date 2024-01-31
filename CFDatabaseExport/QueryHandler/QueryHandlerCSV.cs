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
    /// Handles output of query to CSV file
    /// </summary>
    public class QueryHandlerCSV : IQueryHandler
    {
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsCSV queryOptions = (QueryOptionsCSV)queryOptionsX;

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

                // Write resultset to file
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    // Write header
                    StringBuilder headerLine = new StringBuilder("");
                    for (int fieldIndex = 0; fieldIndex < dataTable.Columns.Count; fieldIndex++)
                    {
                        //ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[fieldIndex].ColumnName);       
                        if (fieldIndex > 0)
                        {
                            headerLine.Append(queryOptions.Delimiter);
                        }                                            
                        headerLine.Append(dataTable.Columns[fieldIndex].ColumnName);                       
                    }
                    writer.WriteLine(headerLine.ToString());

                    // Write data
                    for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                    { 
                        string line = GetLine(queryOptions, dataTable, columnFormats, rowIndex);
                        writer.WriteLine(line);
                    }
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsCSV);
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
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsCSV)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsCSV queryOptions, DataTable dataTable, int column)
        {
            ColumnFormat columnFormat = new ColumnFormat();
            try
            {
                //Controls.ControlOptionsCSV controlOptions = (Controls.ControlOptionsCSV)queryOptions.OptionsUserControl;

                String defaultQuoted = "'";
                columnFormat.ColumnName = dataTable.Columns[column].ColumnName;
                //columnFormat.MapConverter.AddMapping("", "&nbsp;");
                columnFormat.MapConverter.AddMapping(null, queryOptions.NullString);
                string columnType = dataTable.Columns[column].DataType.ToString();

                String format = "";
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

        private string GetLine(QueryOptionsCSV queryOptions, DataTable dataTable, ColumnFormats columnFormats, int rowIndex)
        {
            StringBuilder line = new StringBuilder();
            for (int field = 0; field < dataTable.Columns.Count; field++)
            {
                if (field > 0)
                {
                    line.Append(queryOptions.Delimiter);
                }                
                ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[field].ColumnName);
                string fieldValue = columnFormat.FormatValue(dataTable.Rows[rowIndex][field]);
                line.Append(fieldValue);
            }
            return line.ToString();
        }
    }
}
