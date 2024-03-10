using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Database type
    /// </summary>
    [XmlType("DatabaseType")]
    public class DatabaseType
    {
        [XmlAttribute("ID")]
        public Guid ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
