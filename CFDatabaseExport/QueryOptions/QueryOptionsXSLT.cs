using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    /// <summary>
    /// Handles output of query to file using XSL transform
    /// </summary>
    public class QueryOptionsXSLT : QueryOptions
    {
        public string FileName { get; set; }
        public string TransformFile { get; set; }
    }
}
