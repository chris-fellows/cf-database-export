using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CFDatabaseExport.Models;

namespace CFDatabaseExport.QueryHandlers
{
    /// <summary>
    /// Handles a query. Generates some type of output (E.g. CSV files, display UI grid etc)
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="queryOptions"></param>
        /// <param name="dataTables"></param>
        /// <param name="progress"></param>
        void Handle(SQLQuery query, QueryOptions queryOptions, List<DataTable> dataTables, IProgress progress);

        /// <summary>
        /// Whether class supports the query options
        /// </summary>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        bool Supports(QueryOptions queryOptions);

        /// <summary>
        /// Whether output is visible in UI
        /// </summary>
        bool VisibleOutput { get; }
    }
}
