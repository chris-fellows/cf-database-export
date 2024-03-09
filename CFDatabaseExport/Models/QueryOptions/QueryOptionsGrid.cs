using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Query options for output to grid
    /// </summary>
    public class QueryOptionsGrid : QueryOptions
    {
        public string DateFormat { get; set; }
        public string NullString { get; set; }
        public List<System.Windows.Forms.DataGridView> Grids = new List<System.Windows.Forms.DataGridView>();
    }
}
