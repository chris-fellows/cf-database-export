using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace CFDatabaseExport
{
    /// <summary>
    /// Handles output of query to grid control
    /// </summary>
    public class QueryHandlerGrid : IQueryHandler
    {
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsGrid queryOptions = (QueryOptionsGrid)queryOptionsX;

            // Clear and hide all grids
            foreach(DataGridView dataGridView in queryOptions.Grids)
            {
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();
                dataGridView.Visible = false;
            }

            // Display grids
            for (int dataTableIndex = 0; dataTableIndex < dataTables.Count; dataTableIndex++)
            {
                DataGridView dataGridView = null;
                if (dataTableIndex < queryOptions.Grids.Count)
                {
                    dataGridView = queryOptions.Grids[dataTableIndex];
                }

                if (dataGridView != null)
                {
                    dataGridView.Rows.Clear();
                    dataGridView.Columns.Clear();
                    dataGridView.Visible = true;
                    
                    DataTable dataTable = dataTables[dataTableIndex];
                    ColumnFormats columnFormats = queryOptions.ColumnFormatsList[dataTableIndex];

                    // Add column headers
                    for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                    {
                        dataGridView.Columns.Add(dataTable.Columns[columnIndex].ColumnName, dataTable.Columns[columnIndex].ColumnName);
                    }

                    // Add rows
                    for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                    {
                        dataGridView.Rows.Add(CreateGridRow(queryOptions, dataTable, columnFormats, rowIndex, dataGridView));
                    }
                }
            }            
        }

        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsGrid);
        }

        public bool VisibleOutput
        {
            get { return true; }
        }

        private void SetColumnFormats(QueryOptions queryOptions, List<DataTable> dataTables)
        {
            queryOptions.ColumnFormatsList.Clear();
            foreach (DataTable dataTable in dataTables)
            {
                ColumnFormats columnFormats = new ColumnFormats();
                for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                {
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsGrid)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsGrid queryOptions, DataTable dataTable, int column)
        {
            ColumnFormat columnFormat = new ColumnFormat();
            try
            {
                //Controls.ControlOptionsGrid controlOptions = (Controls.ControlOptionsGrid)queryOptions.OptionsUserControl;
                String defaultQuoted = "'";
                columnFormat.ColumnName = dataTable.Columns[column].ColumnName;
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

        private DataGridViewRow CreateGridRow(QueryOptionsGrid queryOptions, DataTable dataTable, ColumnFormats columnFormats, int rowIndex, DataGridView dataGridView)
        {
            DataGridViewRow row = new DataGridViewRow();
            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                ColumnFormat columnFormat = columnFormats.GetColumnFormatByName(dataTable.Columns[columnIndex].ColumnName);
                using (DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell())
                {
                    cell.Value = columnFormat.FormatValue(dataTable.Rows[rowIndex][columnIndex]);
                    row.Cells.Add(cell);
                }
            }
            return row;
        }     
    }
}
