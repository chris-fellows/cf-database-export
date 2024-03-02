using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CFDatabaseExport
{
    /// <summary>
    /// Database type
    /// </summary>
    public class DatabaseType
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
    }
}
