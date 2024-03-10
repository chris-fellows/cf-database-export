using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using CFDatabaseExport.Models;
using CFDatabaseExport.QueryHandlers;
using CFUtilities.Databases;
using System.Threading;

namespace CFDatabaseExport
{
    /// <summary>
    /// Executes query via SQL
    /// </summary>
    internal class SQLQueryExecutor : IQueryExecutor
    {
        /// <summary>
        /// Runs the query and handles the results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryOptions"></param>
        /// <param name="queryHandler"></param>
        /// <param name="cancellationToken">Cancellation token</param>
        public void ExecuteQuery(Query queryObject, QueryOptions queryOptions, IQueryHandler queryHandler, 
                    IQueryService queryRepository, IQueryFunctionService queryFunctionRepository,
                    CFUtilities.Databases.ISQLGenerator sqlGenerator, IProgress progress,
                    CancellationToken cancellationToken)
        {
            var query = (SQLQuery)queryObject;        

            // Open database
            var databaseConnection = new OleDbDatabase(queryOptions.ConnectionString);
            databaseConnection.Open();

            if (!cancellationToken.IsCancellationRequested)
            {
                // Parse script functions            
                var queryParserService = new QueryParserService();
                string querySql = queryParserService.Parse(databaseConnection, query, queryRepository, queryFunctionRepository, sqlGenerator);

                if (!cancellationToken.IsCancellationRequested)
                {
                    OleDbDataReader reader = null;
                    if (query.HasResultset)
                    {
                        // Run query
                        reader = databaseConnection.ExecuteReader(System.Data.CommandType.Text, querySql, CommandBehavior.Default, null);

                        // Get data tables, one per resultset
                        var dataTables = OleDbDatabase.GetDataTables(reader);
                        //int rowCount = dataTables[0].Rows.Count;

                        // Handle result
                        queryHandler.Handle(query, queryOptions, dataTables, progress, cancellationToken);
                    }
                    else
                    {
                        // Run query
                        databaseConnection.ExecuteNonQuery(System.Data.CommandType.Text, query.SQL, null, null);
                    }
                }
            }
            databaseConnection.Close();
        }
    }
}