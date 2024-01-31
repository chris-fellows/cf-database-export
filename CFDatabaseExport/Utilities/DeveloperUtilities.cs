using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using CFUtilities.XML;
using CFUtilities.CSV;

namespace CFDatabaseExport
{
    class DeveloperUtilities
    {
        public static void CreateDatabaseInfos(string outputFolder)
        {
            ItemList<DatabaseInfo> databaseInfoList = new ItemList<DatabaseInfo>();

            DatabaseInfo databaseInfo = new DatabaseInfo();
            databaseInfo.DisplayName = "Order Database";
            databaseInfo.ConnectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Data\Development\C#\CFDatabaseExport\Data\Databases\Order Database\; Extended Properties = ""text;HDR=Yes;FMT=Delimited""";
            databaseInfoList.Items.Add(databaseInfo);

            //XmlSerialization.SerializeToFile(databaseInfo, Path.Combine(outputFolder, string.Format("Database Info.1.xml")));
            XmlSerialization.SerializeToFile<ItemList<DatabaseInfo>>(databaseInfoList, Path.Combine(outputFolder, "Database Info.xml"));
        }
    
        public static void CreateFunctions(string outputFolder)
        {
            /*
            QueryFunctionListItem item1 = new QueryFunctionListItem();
            item1.Name = "##GenericSelectTableSingle##";
            item1.Prompt = "Select a table";
            item1.ListSQL = "select so.id AS ID, so.name as Description, cast(0 AS BIT) AS IsDefault from sysobjects where so.type='U' so order by so.name ";
            item1.MinItems = 1;
            item1.MaxItems = 1;
            XmlSerialization.SerializeToFile<QueryFunctionListItem>(item1, Path.Combine(outputFolder + @"\Generic", item1.Name + ".xml"));
            */

            QueryFunctionListItem item2 = new QueryFunctionListItem();
            item2.Name = "##GenericSelectTableMultiple##";
            item2.Prompt = "Select one or more tables";
            item2.ListSQL = "select so.id AS ID, so.name as Description, cast(0 AS BIT) AS IsDefault from sysobjects where so.type='U' so order by so.name ";
            //item2.MinItems = 1;
            //item2.MaxItems = 1000;
            XmlSerialization.SerializeToFile<QueryFunctionListItem>(item2, Path.Combine(outputFolder + @"\Generic", item2.Name + ".xml"));
        }
    }
}
