using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CFDatabaseExport.Models
{
    [XmlType("DatabaseInfo")]
    public class DatabaseInfo
    {
        [XmlAttribute("ID")]
        public Guid ID { get; set; }
        [XmlAttribute("DatabaseTypeID")]
        public Guid DatabaseTypeID { get; set; }
        [XmlAttribute("DisplayName")]
        public string DisplayName { get; set; }
        [XmlAttribute("ConnectionString")]
        public string ConnectionString { get; set; }
        [XmlAttribute("SQLGenerator")]
        public string SQLGenerator { get; set; }
    }
}
