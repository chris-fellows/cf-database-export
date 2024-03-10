using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    /// <summary>
    /// Service for available query functions
    /// </summary>
    public interface IQueryFunctionService
    {
        QueryFunction GetByID(Guid id);

        List<QueryFunction> GetAll();

        void Delete(Guid id);

        void Add(QueryFunction queryFunction);

        void Update(QueryFunction queryFunction);

        //List<QueryFunction> GetAll();
        //void Add(List<QueryFunction> queryFunctions);
    }
}
