using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Query that executes when a file is opened. E.g. Open Excel spreadsheet that runs a macro
    /// </summary>
    public class OpenFileQuery : Query
    {
        /// <summary>
        /// File to open. E.g. Excel file that runs a macro when opened.
        /// </summary>
        public string QueryFile { get; set; }
    }
}
