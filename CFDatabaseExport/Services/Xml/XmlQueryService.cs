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
    /// <summary>
    /// Repository for queries stored in file system
    /// </summary>
    public class XmlQueryService : IQueryService
    {
        private string _folder;
        
        public XmlQueryService(string folder)
        {
            _folder = folder;
        }

        public Query GetByID(Guid id)
        {
            return GetAll().FirstOrDefault(q => q.ID == id);
        }      

        public List<Query> GetAll()
        {          
            List<Query> queryList = new List<Query>();                
            foreach (string file in Directory.GetFiles(_folder, "*.xml"))
            {
                queryList.Add(XmlSerialization.DeserializeFromFile<Query>(file));           
            }
            return queryList;
        }

        public void Add(List<Query> queries)
        {
            foreach(var query in queries)
            {
                string filename = Path.Combine(_folder, string.Format("{0}{1}", query.ID, ".xml"));
                XmlSerialization.SerializeToFile<Query>(query, filename);
            }
        }
    }
}
