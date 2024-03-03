using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

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
