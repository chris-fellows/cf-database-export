using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using CFUtilities.Databases;

namespace CFDatabaseExport
{
    internal class SQLQueryExecutor : IQueryExecutor
    {
        /// <summary>
        /// Runs the query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryOptions"></param>
        /// <param name="queryHandler"></param>
        public void ExecuteQuery(Query queryObject, QueryOptions queryOptions, IQueryHandler queryHandler, 
                    IQueryRepository queryRepository, IQueryFunctionRepository queryFunctionRepository,
                    CFUtilities.Databases.ISQLGenerator sqlGenerator, IProgress progress)
        {
            SQLQuery query = (SQLQuery)queryObject;        

            // Open database
            OleDbDatabase databaseConnection = new OleDbDatabase(queryOptions.ConnectionString);
            databaseConnection.Open();

            // Parse script functions            
            QueryParser queryParser = new QueryParser();            
            string querySql = queryParser.Parse(databaseConnection, query, queryRepository, queryFunctionRepository, sqlGenerator);

            OleDbDataReader reader = null;
            if (query.HasResultset)
            {                             
                // Run query
                reader = databaseConnection.ExecuteReader(System.Data.CommandType.Text, querySql, CommandBehavior.Default, null);

                // Get data tables, one per resultset
                List<DataTable> dataTables = OleDbDatabase.GetDataTables(reader);
                int rowCount = dataTables[0].Rows.Count;

                // Handle result
                queryHandler.Handle(query, queryOptions, dataTables, progress);
            }
            else
            {
                // Run query
                databaseConnection.ExecuteNonQuery(System.Data.CommandType.Text, query.SQL, null, null);
            }
            databaseConnection.Close();
        }
    }
}