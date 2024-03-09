using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Models
{
    public class QueryOptionsJSON : QueryOptions
    {
        public string DateFormat { get; set; }
        public string NullString { get; set; }
        public string FileName { get; set; }
    }
}
