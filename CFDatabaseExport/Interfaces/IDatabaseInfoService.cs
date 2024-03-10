using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDatabaseExport.Models;

namespace CFDatabaseExport
{
    public interface IDatabaseInfoService
    {
        DatabaseInfo GetByID(Guid id);

        List<DatabaseInfo> GetAll();

        void Delete(Guid id);

        void Add(DatabaseInfo databaseInfo);

        void Update(DatabaseInfo databaseInfo);
    }
}
