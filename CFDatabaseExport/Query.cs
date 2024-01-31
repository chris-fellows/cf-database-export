using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CFDatabaseExport
{
    /// <summary>
    /// Query
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(SQLQuery))]
    [XmlInclude(typeof(OpenFileQuery))]
    public abstract class Query
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid DatabaseID { get; set; }
    }
}
