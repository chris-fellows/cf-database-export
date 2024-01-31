using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    /// <summary>
    /// Query options for output to SQL file using template
    /// </summary>
    public class QueryOptionsSQL : QueryOptions
    {
        public string DateFormat { get; set; }
        public string NullString { get; set; }
        public string TemplateSQLFile { get; set; }
        public string RowTemplateSQL { get; set; }       
        public string FileName { get; set; }
    }
}
