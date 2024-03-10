using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    /// <summary>
    /// Interface for queries
    /// </summary>
    public interface IQueryService
    {
        Query GetByID(Guid id);

        List<Query> GetAll();

        void Delete(Guid id);

        void Add(Query query);

        void Update(Query query);
    }
}
