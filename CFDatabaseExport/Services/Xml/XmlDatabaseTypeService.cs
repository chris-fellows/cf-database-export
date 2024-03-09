using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CFDatabaseExport.Models;
using CFUtilities.XML;

namespace CFDatabaseExport
{
    public class XmlDatabaseTypeService : IDatabaseTypeService
    {
        private string _folder;

        public XmlDatabaseTypeService(string folder)
        {
            _folder = folder;
        }

        public List<DatabaseType> GetAll()
        {
            List<DatabaseType> databaseTypes = new List<DatabaseType>();
            foreach (string file in Directory.GetFiles(_folder, "*.xml", SearchOption.AllDirectories))
            {
                databaseTypes.Add(XmlSerialization.DeserializeFromFile<DatabaseType>(file));
            }
            return databaseTypes;
        }

        public void Add(List<DatabaseType> databaseTypes)
        {
            foreach (var databaseType in databaseTypes)
            {
                string filename = Path.Combine(_folder, string.Format("{0}{1}", databaseType.ID, ".xml"));
                XmlSerialization.SerializeToFile<DatabaseType>(databaseType, filename);
            }
        }
    }
}
