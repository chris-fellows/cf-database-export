using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace CFDatabaseExport.Models
{
    [Serializable]
    public class SQLQuery : Query
    {
        public string QueryFile { get; set; }
        public bool HasResultset { get; set; }

        public string SQL
        {
            get
            {
                return File.ReadAllText(this.QueryFile);
            }
        }        
    }
}
