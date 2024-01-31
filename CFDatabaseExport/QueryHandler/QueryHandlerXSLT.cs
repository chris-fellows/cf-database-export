using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace CFDatabaseExport
{
    /// <summary>
    /// Handles output of query to file using XSL transform
    /// </summary>
    public class QueryHandlerXSLT : IQueryHandler
    {
        public void Handle(SQLQuery query, QueryOptions queryOptionsX, List<DataTable> dataTables, IProgress progress)
        {
            SetColumnFormats(queryOptionsX, dataTables);
            QueryOptionsXSLT queryOptions = (QueryOptionsXSLT)queryOptionsX;

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

                //StreamWriter fileWriter = new StreamWriter(@"C:\Temp\Data.xml", true);
                //dataTable.TableName = "Row";
                //dataTable.WriteXml(fileWriter);
                //fileWriter.Flush();
                //fileWriter.Close();

                // Convert data table to XML
                dataTable.TableName = "Row";
                MemoryStream stream = new MemoryStream();
                dataTable.WriteXml(stream);
                stream.Position = 0;

                // Load transform             
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(queryOptions.TransformFile);

                // Execute the transform and output the results to a file.
                using (FileStream writer = new FileStream(filename, FileMode.CreateNew))
                {
                    using (XmlReader xmlReader = XmlReader.Create(stream))
                    {
                        xslt.Transform(xmlReader, (XsltArgumentList)null, writer);
                    }
                }
            }
        }

        public bool Supports(QueryOptions queryOptions)
        {
            return (queryOptions is QueryOptionsXSLT);
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
                    ColumnFormat columnFormat = GetDefaultColumnFormat((QueryOptionsXSLT)queryOptions, dataTable, columnIndex);
                    columnFormats.Formats.Add(columnFormat);
                }
                queryOptions.ColumnFormatsList.Add(columnFormats);
            }
        }

        private ColumnFormat GetDefaultColumnFormat(QueryOptionsXSLT queryOptions, DataTable dataTable, int column)
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
