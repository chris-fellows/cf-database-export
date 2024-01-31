using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    /// <summary>
    /// Query options for output to CSV
    /// </summary>
    public class QueryOptionsCSV : QueryOptions
    {
        public string DateFormat { get; set; }
        public string NullString { get; set; }
        public string FileName { get; set; }
        public Char Delimiter { get; set; }      
    }
}
