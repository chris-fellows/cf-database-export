using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    public interface IDatabaseTypeRepository
    {
        List<DatabaseType> GetAll();
    }
}
