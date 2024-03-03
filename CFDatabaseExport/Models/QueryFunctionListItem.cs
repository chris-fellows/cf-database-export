using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Query function that lets user select one or more items from a list
    /// </summary>
    [XmlType("QueryFunctionItem")]
    public class QueryFunctionListItem : QueryFunction
    {        
        [XmlAttribute("Prompt")]
        public string Prompt { get; set; }      // Prompt displayed to user
        [XmlElement("ListSQL")]
        public string ListSQL { get; set; }     // Returns SQL for list
        [XmlAttribute("ValueType")]
        public string ValueType { get; set; }
    }
}
