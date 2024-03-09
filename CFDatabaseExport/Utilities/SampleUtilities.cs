using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDatabaseExport.Models;

namespace CFDatabaseExport.Utilities
{
    /// <summary>
    /// Utilities for creating sample queries
    /// </summary>
    internal class SampleUtilities
    {
        public static void CreateQueries()
        {
            string oldQueryFolder = @"C:\Data\Dev\C#\CFDatabaseExport\bin\Debug\Data\Queries\Order Database";
            string newQueryFolder = @"C:\Data\Dev\C#\CFDatabaseExport\bin\Debug\Data\Queries";

            Guid ordersDatabaseId = Guid.NewGuid();

            IQueryService queryRepository = new XmlQueryService(newQueryFolder);

            foreach(string queryFile in System.IO.Directory.GetFiles(oldQueryFolder, "*.sql"))
            {
                SQLQuery sqlQuery = new SQLQuery()
                {
                    DatabaseID = ordersDatabaseId,
                    ID = Guid.NewGuid(),
                    HasResultset = true,
                    Name = System.IO.Path.GetFileNameWithoutExtension(queryFile),
                    QueryFile = System.IO.Path.GetFileName(queryFile)                    
                };
                queryRepository.Add(new List<Query>() { sqlQuery });
            }
        }

        public static void CreateQueryFunctions(ApplicationObject applicationObject)
        {
            IQueryFunctionService queryFunctionRepository = new XmlQueryFunctionService(applicationObject.QueryFunctionFolder);

            QueryFunctionListItem queryFunction1 = new QueryFunctionListItem()
            {
                ID = Guid.NewGuid(),                
                ListSQL = "select id, name from countries",            
                Name = "SelectCountry",
                Prompt = "Select country"
            };
            queryFunctionRepository.Add(new List<QueryFunction>() { queryFunction1 });

            QueryFunctionListItem queryFunction2 = new QueryFunctionListItem()
            {
                ID = Guid.NewGuid(),
                ListSQL = "select id, name from customers",           
                Name = "SelectCustomer",
                Prompt = "Select customer"
            };
            queryFunctionRepository.Add(new List<QueryFunction>() { queryFunction2 });
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
