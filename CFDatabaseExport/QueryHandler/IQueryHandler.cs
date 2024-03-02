using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CFDatabaseExport
{
    /// <summary>
    /// Handles a query. Generates some type of output (E.g. CSV files, display UI grid etc)
    /// </summary>
    interface IQueryHandler
    {
        void Handle(SQLQuery query, QueryOptions queryOptions, List<DataTable> dataTables, IProgress progress);
        bool Supports(QueryOptions queryOptions);
        bool VisibleOutput { get; }
    }
}
