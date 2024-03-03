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
    public class XmlQueryFunctionRepository : IQueryFunctionRepository
    {
        private string _folder;

        public XmlQueryFunctionRepository(string folder)
        {
            _folder = folder;
        }

        public List<QueryFunction> GetAll()
        {
            List<QueryFunction> queryFunctions = new List<QueryFunction>();
            foreach (string file in Directory.GetFiles(_folder, "*.xml", SearchOption.AllDirectories))
            {
                queryFunctions.Add(XmlSerialization.DeserializeFromFile<QueryFunction>(file));
            }
            return queryFunctions;
        }

        public void Add(List<QueryFunction> queryFunctions)
        {
            foreach (var queryFunction in queryFunctions)
            {
                string filename = Path.Combine(_folder, string.Format("{0}{1}", queryFunction.ID, ".xml"));
                XmlSerialization.SerializeToFile<QueryFunction>(queryFunction, filename);
            }
        }
    }
}
