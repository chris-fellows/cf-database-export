using System;
using System.Collections.Generic;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    public interface IDatabaseTypeService
    {
        DatabaseType GetByID(Guid id);

        List<DatabaseType> GetAll();

        void Delete(Guid id);

        void Add(DatabaseType databaseType);

        void Update(DatabaseType databaseType);
    }
}
