using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CFDatabaseExport
{
    interface IQueryHandler
    {
        void Handle(SQLQuery query, QueryOptions queryOptions, List<DataTable> dataTables, IProgress progress);
        bool Supports(QueryOptions queryOptions);
        bool VisibleOutput { get; }
    }
}
