using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    abstract public class QueryOptions
    {
        public int Timeout { get; set; }
        public string ConnectionString { get; set; }
        public List<ColumnFormats> ColumnFormatsList = new List<ColumnFormats>();
    }
}
