using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    public class LocalNameValuePair
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public LocalNameValuePair(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
