using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CFDatabaseExport.Models;
using CFUtilities.XML;
using CFDatabaseExport.Services.Xml;

namespace CFDatabaseExport
{
    public class XmlDatabaseTypeService : XmlBaseService<DatabaseType, Guid>, IDatabaseTypeService
    {
        //private string _folder;

        public XmlDatabaseTypeService(string folder) : base(folder,
                                 (Guid id) => $"{id}.xml",
                                 (DatabaseType databaseType) => databaseType.ID)
        {

        }

        //public XmlDatabaseTypeService(string folder)
        //{
        //    _folder = folder;
        //}

        //public List<DatabaseType> GetAll()
        //{
        //    List<DatabaseType> databaseTypes = new List<DatabaseType>();
        //    foreach (string file in Directory.GetFiles(_folder, "*.xml", SearchOption.AllDirectories))
        //    {
        //        databaseTypes.Add(XmlSerialization.DeserializeFromFile<DatabaseType>(file));
        //    }
        //    return databaseTypes;
        //}

        //public void Add(List<DatabaseType> databaseTypes)
        //{
        //    foreach (var databaseType in databaseTypes)
        //    {
        //        string filename = Path.Combine(_folder, string.Format("{0}{1}", databaseType.ID, ".xml"));
        //        XmlSerialization.SerializeToFile<DatabaseType>(databaseType, filename);
        //    }
        //}
    }
}
