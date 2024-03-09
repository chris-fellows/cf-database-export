using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    public interface IQueryFunctionService
    {
        List<QueryFunction> GetAll();
        void Add(List<QueryFunction> queryFunctions);
    }
}
