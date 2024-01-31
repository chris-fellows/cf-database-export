using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    /// <summary>
    /// Interface for executing a query
    /// </summary>
    internal interface IQueryExecutor
    {
        void ExecuteQuery(Query queryObject, QueryOptions queryOptions, IQueryHandler queryHandler, 
                    IQueryRepository queryRepository, IQueryFunctionRepository queryFunctionRepository,
                    CFUtilities.Databases.ISQLGenerator sqlGenerator, IProgress progress);
    }
}
