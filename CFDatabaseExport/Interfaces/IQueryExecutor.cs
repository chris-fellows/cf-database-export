using System;
using System.Threading;
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
                    IQueryService queryRepository, IQueryFunctionService queryFunctionRepository,
                    CFUtilities.Databases.ISQLGenerator sqlGenerator, IProgress progress,
                    CancellationToken cancellationToken);
    }
}
