using System;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    internal class ApplicationObject
    {
        public static string DateTimeFormat1 = "dd-MM-yyyy HH:mm:ss FFFF";
        public static Char Quote = '\'';
        //public string DemoDatabaseDisplayName = "Order Database";

        public static string DatabaseInfoFolder
        {
            get
            {
                return string.Format(@"{0}\Data\Database Info", Environment.CurrentDirectory);                
            }
        }

        public static string DatabaseTypeFolder
        {
            get
            {
                return string.Format(@"{0}\Data\Database Types", Environment.CurrentDirectory);
            }
        }

        public static string QueryFunctionFolder
        {
            get
            {
                return string.Format(@"{0}\Data\Query Functions", Environment.CurrentDirectory);
            }
        }

        public static string QueryFolder
        {
            get
            {
                return string.Format(@"{0}\Data\Queries", Environment.CurrentDirectory);
            }
        }

        public static string OutputFolder
        {
            get
            {
                return string.Format(@"{0}\Data\Output", Environment.CurrentDirectory);
            }
        }

        public static string DefaultHTMLTemplateFile
        {
            get
            {
                return string.Format(@"{0}\Data\Templates\HTML\Template 1.htm", Environment.CurrentDirectory);
            }
        }

        public static string DefaultXSLTransformFile
        {
            get
            {
                return string.Format(@"{0}\Data\Templates\XSLT\Customer list.xslt", Environment.CurrentDirectory);               
            }
        }

        public static string DefaultTemplateSQLFile
        {
            get
            {
                return string.Format(@"{0}\Data\Templates\SQL\Customer list.sql", Environment.CurrentDirectory);
            }
        }       

        public static string GetQueriesLocationID(DatabaseInfo databaseInfo)
        {            
            return string.Format(@"{0}\Data\Queries\{1}", Environment.CurrentDirectory, databaseInfo.DisplayName);           
        }     
    }
}
