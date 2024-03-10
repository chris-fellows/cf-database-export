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
        List<QueryFunction> GetAll();
        void Add(List<QueryFunction> queryFunctions);
    }
}
