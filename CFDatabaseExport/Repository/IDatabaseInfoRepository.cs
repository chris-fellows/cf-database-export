using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    public interface IDatabaseInfoRepository
    {
        List<DatabaseInfo> GetAll();
    }
}
