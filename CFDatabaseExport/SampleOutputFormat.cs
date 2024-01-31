using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    public class SampleOutputFormat
    {
        public string DisplayName { get; set; }
        public DatabaseInfo DatabaseInfo { get; set; }
        public SQLQuery SQLQuery { get; set; }
        public OutputFormat OutputFormat { get; set; }

        public SampleOutputFormat(string displayName, DatabaseInfo databaseInfo, SQLQuery sqlQuery, OutputFormat outputFormat)
        {
            DisplayName = displayName;
            DatabaseInfo = databaseInfo;
            SQLQuery = sqlQuery;
            OutputFormat = outputFormat;
        }
    }
}
