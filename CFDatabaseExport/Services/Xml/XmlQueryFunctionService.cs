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
    public class XmlQueryFunctionService : XmlBaseService<QueryFunction, Guid>, IQueryFunctionService
    {
        public XmlQueryFunctionService(string folder) : base(folder,
                             (Guid id) => $"{id}.xml",
                             (QueryFunction queryFunction) => queryFunction.ID)
        {

        }

        //private string _folder;

        //public XmlQueryFunctionService(string folder)
        //{
        //    _folder = folder;
        //}

        //public List<QueryFunction> GetAll()
        //{
        //    List<QueryFunction> queryFunctions = new List<QueryFunction>();
        //    foreach (string file in Directory.GetFiles(_folder, "*.xml", SearchOption.AllDirectories))
        //    {
        //        queryFunctions.Add(XmlSerialization.DeserializeFromFile<QueryFunction>(file));
        //    }
        //    return queryFunctions;
        //}

        //public void Add(List<QueryFunction> queryFunctions)
        //{
        //    foreach (var queryFunction in queryFunctions)
        //    {
        //        string filename = Path.Combine(_folder, string.Format("{0}{1}", queryFunction.ID, ".xml"));
        //        XmlSerialization.SerializeToFile<QueryFunction>(queryFunction, filename);
        //    }
        //}
    }
}
