using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    internal class OpenFileQueryExecutor : IQueryExecutor
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
            throw new NotImplementedException();
        }
    }
}
