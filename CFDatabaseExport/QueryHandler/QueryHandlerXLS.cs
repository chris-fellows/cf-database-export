using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using CarlosAg.ExcelXmlWriter;
using CFDatabaseExport.Models;

namespace CFDatabaseExport.QueryHandlers
{
    /// <summary>
    /// Handles output of query to XLS file
    /// </summary>
    public class QueryHandlerXLS : IQueryHandler
    {
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            /*
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets.Add("Sample");
            WorksheetRow row = sheet.Table.Rows.Add();
            row.Cells.Add("Hello World");
            book.Save(@"c:\test.xls");
            */

            Workbook workbook = new Workbook();

            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsXLS queryOptions = (QueryOptionsXLS)queryOptionsX;

            int recordsetCount = 0;
            for (int dataTableIndex = 0; dataTableIndex < dataTables.Count; dataTableIndex++)
            {
                Worksheet worksheet = workbook.Worksheets.Add(string.Format("Data {0}", dataTableIndex + 1));

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
                /*
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                */

                // If multiple recordsets but there's no {sequence} placeholder then ensure unique filename set
                if (recordsetCount > 1 && !queryOptions.FileName.Contains("{sequence}"))
                {
                    filename = string.Format(@"{0}\{1}.{2}{3}", Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename), recordsetCount, Path.GetExtension(filename));
                }

                // Write resultset to file

                // Write header
                WorksheetRow headerRow = worksheet.Table.Rows.Add();
                StringBuilder headerLine = new StringBuilder("");
                    for (int fieldIndex = 0; fieldIndex < dataTable.Columns.Count; fieldIndex++)
                    {
                        //ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[fieldIndex].ColumnName);       
                        headerRow.Cells.Add(dataTable.Columns[fieldIndex].ColumnName);
                        headerLine.Append(dataTable.Columns[fieldIndex].ColumnName);
                    }
                //writer.WriteLine(headerLine.ToString());

                // Write data
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    WorksheetRow row = GetLine(worksheet, queryOptions, dataTable, columnFormats, rowIndex);              
                }
                                
            }

            workbook.Save(queryOptions.FileName);
        }

        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsXLS);
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
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsXLS)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsXLS queryOptions, DataTable dataTable, int column)
        {
            ColumnFormat columnFormat = new ColumnFormat();
            try
            {
                //Controls.ControlOptionsCSV controlOptions = (Controls.ControlOptionsCSV)queryOptions.OptionsUserControl;

                String defaultQuoted = "'";
                columnFormat.ColumnName = dataTable.Columns[column].ColumnName;
                //columnFormat.MapConverter.AddMapping("", "&nbsp;");
                //columnFormat.MapConverter.AddMapping(null, queryOptions.NullString);
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
                        //columnFormat.FormatConverter.SetFormat(queryOptions.DateFormat);
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

        private WorksheetRow GetLine(Worksheet worksheet, QueryOptionsXLS queryOptions, DataTable dataTable, ColumnFormats columnFormats, int rowIndex)
        {
            WorksheetRow row = worksheet.Table.Rows.Add();
                        
            for (int field = 0; field < dataTable.Columns.Count; field++)
            {              
                ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[field].ColumnName);
                string fieldValue = columnFormat.FormatValue(dataTable.Rows[rowIndex][field]);
                row.Cells.Add(fieldValue);

            
            }
            return row;
        }
    }
}
