using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CFDatabaseExport.Models;
using CFUtilities.XML;
using CFUtilities;
using CFDatabaseExport.Services.Xml;

namespace CFDatabaseExport
{
    public class XmlDatabaseInfoService : XmlBaseService<DatabaseInfo, Guid>, IDatabaseInfoService
    {        
        public XmlDatabaseInfoService(string folder) : base(folder,
                                 (Guid id) => $"{id}.xml",
                                 (DatabaseInfo databaseInfo) => databaseInfo.ID)
        {
     
        }

        /*
        public List<DatabaseInfo> GetAll()
        {            
            var list = Directory.GetFiles(_folder, "*.xml").Select(item => XmlSerialization.DeserializeFromFile<DatabaseInfo>(item)).ToList();
            return list;            

            /*
            string filename = Path.Combine(_folder, "Database Info.xml");
            ItemList<DatabaseInfo> databaseInfoList = XmlSerialization.DeserializeFromFile<ItemList<DatabaseInfo>>(filename);
            return databaseInfoList.Items;            
        }
        */
        
        /*
        public void Delete(Guid id)
        {            
            IOUtilities.DeleteFiles(new List<string>() { GetFile(id) });
        }

        public void Add(DatabaseInfo databaseInfo)
        {
            var file = GetFile(databaseInfo.ID);
            XmlSerialization.SerializeToFile(databaseInfo, file, typeof(DatabaseInfo));            
        }

        public void Update(DatabaseInfo databaseInfo)
        {
            var file = GetFile(databaseInfo.ID);
            XmlSerialization.SerializeToFile(databaseInfo, file, typeof(DatabaseInfo));
        }
        */
    }
}
