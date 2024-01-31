using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFUtilities.ValueConverters;

namespace CFDatabaseExport
{
    public class ColumnFormat
    {
        public string ColumnName;

        public ValueConverter1<Object> FormatConverter = new ValueConverter1<Object>();  // Converts values by formatting string
        public ValueConverter2<Object> MapConverter = new ValueConverter2<Object>();     // Converts values by mapping table (E.g. True="Yes", False="No")
        public String Quoted = "";        
    
        public String FormatValue(Object value) {
            String formattedValue = "";
        
            // Check if there's a standard converter (E.g. NULL converts to "NULL")
            if (this.MapConverter.CanConvertValue(value)) {
                formattedValue = this.MapConverter.ConvertValue(value);
            }
            else {
                // Use format converter
                formattedValue = this.FormatConverter.ConvertValue(value);
            }
        
            // Add quotes if necessary
            if (this.Quoted.Length > 0) {
                formattedValue = this.Quoted + formattedValue + this.Quoted;
            }                
            return formattedValue;
        }
    }
}
