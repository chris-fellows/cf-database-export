using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Query function
    /// </summary>
    [XmlType("QueryFunction")]
    [XmlInclude(typeof(QueryFunctionListItem))]
    public abstract class QueryFunction
    {
        [XmlAttribute("ID")]
        public Guid ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Database types that the function is applicable to. Some functions may be global. E.g. Select a date
        /// from a list of dates.
        /// </summary>
        [XmlArray("DatabaseTypes")]
        public List<Guid> DatabaseTypeIDs { get; set; }
    }
}
