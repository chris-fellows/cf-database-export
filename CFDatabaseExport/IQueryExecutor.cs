using System;
using CFDatabaseExport.Models;
using CFDatabaseExport.QueryHandlers;

namespace CFDatabaseExport
{
    /// <summary>
    /// Interface for executing a query and creating the output
    /// </summary>
    internal interface IQueryExecutor
    {
        void ExecuteQuery(Query queryObject, QueryOptions queryOptions, IQueryHandler queryHandler, 
                    IQueryRepository queryRepository, IQueryFunctionRepository queryFunctionRepository,
                    CFUtilities.Databases.ISQLGenerator sqlGenerator, IProgress progress);
    }
}
