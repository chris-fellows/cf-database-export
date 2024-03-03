using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    public interface IQueryFunctionRepository
    {
        List<QueryFunction> GetAll();
        void Add(List<QueryFunction> queryFunctions);
    }
}
