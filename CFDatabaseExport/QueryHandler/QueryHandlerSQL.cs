using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CFDatabaseExport.Models;

namespace CFDatabaseExport.QueryHandlers
{
    /// <summary>
    /// Handles output of query to SQL file from template
    /// </summary>
    public class QueryHandlerSQL : IQueryHandler
    {
        public QueryHandlerSQL()
        {
            
        }
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsSQL queryOptions = (QueryOptionsSQL)queryOptionsX;

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
                StringBuilder lines = new StringBuilder("");
                // Write data
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    string line = GetLine(queryOptions, dataTable, columnFormats, rowIndex);
                    lines.Append(line);
                }

                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    // Replace row templates
                    string sql = File.ReadAllText(queryOptions.TemplateSQLFile);
                    sql = sql.Replace("##ROW_TEMPLATES##", lines.ToString());

                    writer.Write(sql);
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsSQL);
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
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsSQL)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsSQL queryOptions, DataTable dataTable, int column)
        {
            ColumnFormat columnFormat = new ColumnFormat();
            try
            {
                //Controls.ControlOptionsSQL controlOptions = (Controls.ControlOptionsSQL)queryOptions.OptionsUserControl;
                String defaultQuoted = "'";
                columnFormat.ColumnName = dataTable.Columns[column].ColumnName;
                //columnFormat.MapConverter.AddMapping("", "&nbsp;");
                columnFormat.MapConverter.AddMapping(null, queryOptions.NullString);
                string columnType = dataTable.Columns[column].DataType.ToString();
            
                switch (columnType)
                {
                    case "System.Guid":
                        break;
                    case "System.Boolean":
                        columnFormat.MapConverter.AddMapping(true, "1");
                        columnFormat.MapConverter.AddMapping(false, "0");
                        break;
                    case "System.Byte":
                        break;
                    case "System.Char":
                        columnFormat.Quoted = defaultQuoted;
                        break;
                    case "System.DateTime":
                        columnFormat.Quoted = defaultQuoted;
                        columnFormat.FormatConverter.SetFormat(queryOptions.DateFormat);
                        break;
                    case "System.Double":
                        break;
                    case "System.Single":
                        break;
                    case "System.String":
                        columnFormat.Quoted = defaultQuoted;
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

        private string GetLine(QueryOptionsSQL queryOptions, DataTable dataTable, ColumnFormats columnFormats, int rowIndex)
        {
            string line = queryOptions.RowTemplateSQL;

            for (int field = 0; field < dataTable.Columns.Count; field++)
            {
                ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[field].ColumnName);
                string fieldValue = columnFormat.FormatValue(dataTable.Rows[rowIndex][field]);
                //bool quoted = IsColumnNeedsQuoting(dataTable, field);

                // Replace field name by index
                string placeholder1 = string.Format("##COLUMN_NAME_{0}##", field + 1);
                line = line.Replace(placeholder1, FormatSQLObjectName(dataTable.Columns[field].ColumnName));

                // Replace field name by name (Really necessary?)
                string placeholder4 = string.Format("##COLUMN_NAME_{0}##", dataTable.Columns[field].ColumnName);
                line = line.Replace(placeholder4, FormatSQLObjectName(dataTable.Columns[field].ColumnName));

                // Replace field value by index
                //string value = GetFieldValue(dataTable, rowIndex, field, queryOptions.NullValueString, queryOptions.DateFormat, quoted, queryOptions.Quote);
                string value = fieldValue;
                string placeholder2 = string.Format("##COLUMN_VALUE_{0}##", field + 1);                
                line = line.Replace(placeholder2, value);

                // Replace field value by name
                string placeholder3 = string.Format("##COLUMN_VALUE_{0}##", dataTable.Columns[field].ColumnName);                
                line = line.Replace(placeholder3, value);                
            }
            return line.ToString() + Environment.NewLine;
        }

        private static string FormatSQLObjectName(string name)
        {
            return name.StartsWith("[") ? name : "[" + name + "]";
        }

        //protected override string GetFieldValue(DataTable dataTable, int rowIndex, int fieldIndex, string nullValueString, string dateFormat, bool quoted, Char quote)
        //{
        //    if ((dataTable.Rows[rowIndex][fieldIndex] == DBNull.Value || dataTable.Rows[rowIndex][fieldIndex] == null))
        //    {
        //        return nullValueString;
        //    }
        //    string value = "";
        //    if (dataTable.Columns[fieldIndex].DataType == typeof(DateTime))
        //    {
        //        DateTime dateTime = (DateTime)dataTable.Rows[rowIndex][fieldIndex];
        //        value = dateTime.ToString(dateFormat);
        //    }            
        //    else if (dataTable.Columns[fieldIndex].DataType == typeof(Boolean))
        //    {
        //        value = Convert.ToBoolean(dataTable.Rows[rowIndex][fieldIndex]) ? "1" : "0";
        //    }
        //    else
        //    {
        //        value = dataTable.Rows[rowIndex][fieldIndex].ToString();
        //    }
        //    if (quoted)
        //    {
        //        value = string.Format("{0}{1}{0}", quote, value);
        //    }
        //    return value;
        //}
    }
}
