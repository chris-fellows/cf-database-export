using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    /// <summary>
    /// Interface for queries
    /// </summary>
    public interface IQueryRepository
    {   
        Query GetByID(Guid id);
        List<Query> GetAll();
        void Add(List<Query> queries);
    }
}
