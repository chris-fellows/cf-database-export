using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDatabaseExport.Interfaces;
using CFDatabaseExport.Models;
using CFUtilities.XML;

namespace CFDatabaseExport.Demo.Order
{
    /// <summary>
    /// Utilities for creating sample queries
    /// </summary>
    internal class OrderUtilities : IDatabaseUtilities
    {
        public DatabaseType CreateDatabaseType(IDatabaseTypeService databaseTypeService)
        {
            DatabaseType databaseType = new DatabaseType()
            {
                ID =Guid.NewGuid(),
                Name = "Order Databases"
            };
            databaseTypeService.Add(databaseType);

            return databaseType;
        }

        public DatabaseInfo CreateDatabaseInfo(IDatabaseInfoService databaseInfoService, IDatabaseTypeService databaseTypeService,
                            string databaseFolder)
        {
            //ItemList<DatabaseInfo> databaseInfoList = new ItemList<DatabaseInfo>();

            var databaseType = databaseTypeService.GetAll().First(dt => dt.Name == "Order Databases");

            Char quotes = '"';
            DatabaseInfo databaseInfo = new DatabaseInfo();
            databaseInfo.ID = Guid.NewGuid();
            databaseInfo.DatabaseTypeID = databaseType.ID;
            databaseInfo.DisplayName = SampleDatabaseDisplay;
            databaseInfo.Description = "Demo database for orders";
            //databaseInfo.ConnectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Data\Development\C#\CFDatabaseExport\Data\Databases\Order Database\; Extended Properties = ""text;HDR=Yes;FMT=Delimited""";
            databaseInfo.ConnectionString = $"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {databaseFolder}\\; Extended Properties = {quotes}text;HDR=Yes;FMT=Delimited{quotes}";
            databaseInfo.SQLGenerator = "MSSQLGenerator";

            databaseInfoService.Add(databaseInfo);

            //databaseInfoList.Items.Add(databaseInfo);

            //XmlSerialization.SerializeToFile(databaseInfo, Path.Combine(outputFolder, string.Format("Database Info.1.xml")));
            //XmlSerialization.SerializeToFile<ItemList<DatabaseInfo>>(databaseInfoList, Path.Combine(outputFolder, "Database Info.xml"));

            return databaseInfo;
        }

        public void CreateQueries(IDatabaseInfoService databaseInfoService, string newQueryFolder, string oldQueryFolder, IQueryService queryService)
        {
            Directory.CreateDirectory(newQueryFolder);

            var databaseInfo = databaseInfoService.GetAll().FirstOrDefault(di => di.DisplayName == SampleDatabaseDisplay);           

            // Copy old file to new folder
            foreach (string oldQueryFile in System.IO.Directory.GetFiles(oldQueryFolder, $"{SampleDatabaseDisplay}.*.sql"))
            {
                var newQueryFile = Path.Combine(newQueryFolder, Path.GetFileName(oldQueryFile));
                File.Copy(oldQueryFile, newQueryFile);
            }

            // Add Query XML
            foreach (string queryFile in System.IO.Directory.GetFiles(newQueryFolder, "*.sql"))
            {
                SQLQuery sqlQuery = new SQLQuery()
                {
                    DatabaseID = databaseInfo.ID,
                    ID = Guid.NewGuid(),
                    HasResultset = true,
                    Name = System.IO.Path.GetFileNameWithoutExtension(queryFile),
                    QueryFile = queryFile       //  System.IO.Path.GetFileName(queryFile)                    
                };
                queryService.Add(sqlQuery);
                //queryRepository.Add(new List<Query>() { sqlQuery });
            }
        }

        public void CreateQueryFunctions(IQueryFunctionService queryFunctionService)
        {
            //IQueryFunctionService queryFunctionRepository = new XmlQueryFunctionService(applicationObject.QueryFunctionFolder);

            QueryFunctionListItem queryFunction1 = new QueryFunctionListItem()
            {
                ID = Guid.NewGuid(),                
                ListSQL = "select id, name from countries",            
                Name = "SelectCountry",
                Prompt = "Select country"
            };
            queryFunctionService.Add(queryFunction1);

            QueryFunctionListItem queryFunction2 = new QueryFunctionListItem()
            {
                ID = Guid.NewGuid(),
                ListSQL = "select id, name from customers",           
                Name = "SelectCustomer",
                Prompt = "Select customer"                
            };
            queryFunctionService.Add(queryFunction2);
        }

        public static string SampleDatabaseDisplay
        {
            get { return "Order Database"; }
        }

        public static string SampleQueriesLocationID
        {
            get
            {
                return string.Format(@"{0}\Data\Queries\Order Database", Environment.CurrentDirectory);
            }
        }

        public static string SampleXSLQueryFileName
        {
            get
            {
                return "Order list";
            }
        }

        public static string SampleXSLTransformFile
        {
            get
            {
                return string.Format(@"{0}\Data\Templates\XSLT\Order list.xslt", Environment.CurrentDirectory);
            }
        }
    }
}
