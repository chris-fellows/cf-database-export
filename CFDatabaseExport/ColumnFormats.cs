using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    public class ColumnFormats
    {
        public List<ColumnFormat> Formats = new List<ColumnFormat>();
    
        public ColumnFormat GetColumnFormatByName(String name)
        {
            foreach (ColumnFormat columnFormat in Formats) 
            {
                if (columnFormat.ColumnName.Equals(name))
                {
                    return columnFormat;
                }            
            }    
            return null;
        }
    }
}
