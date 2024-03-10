using CFDatabaseExport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Interfaces
{
    public interface IDatabaseUtilities
    {
        DatabaseType CreateDatabaseType(IDatabaseTypeService databaseTypeService);

        DatabaseInfo CreateDatabaseInfo(IDatabaseInfoService databaseInfoService, IDatabaseTypeService databaseTypeService,
                            string databaseFolder);

        void CreateQueries(IDatabaseInfoService databaseInfoService, string newQueryFolder, string oldQueryFolder, IQueryService queryService);

        void CreateQueryFunctions(IQueryFunctionService queryFunctionService);
    }
}
