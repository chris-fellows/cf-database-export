using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CFDatabaseExport.Models
{
    [XmlType("ItemList")] 
    public class ItemList<T>
    {
        [XmlArray("Items")]
        //[XmlArrayItem("Item")]
        public List<T> Items = new List<T>();
    }
}
