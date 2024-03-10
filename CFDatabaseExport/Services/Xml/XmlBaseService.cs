using CFDatabaseExport.Models;
using CFUtilities.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Services.Xml
{
    /// <summary>
    /// Base class for serialized item
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TIDType"></typeparam>
    public abstract class XmlBaseService<TItem, TIDType>
    {
        protected string _folder;
        protected Func<TIDType, string> _getFileFunction;   // Returns file name for ID  
        protected Func<TItem, TIDType> _getIdFunction;      // Returns ID for item

        public XmlBaseService(string folder, 
                        Func<TIDType, string> getFileFunction,
                        Func<TItem, TIDType> getIdFunction)
        {
            _folder = folder;
            _getFileFunction = getFileFunction;
            _getIdFunction = getIdFunction;
        }   

        public virtual TItem GetByID(TIDType id)
        {
            var file = Path.Combine(_folder, _getFileFunction(id));
            if (File.Exists(file))
            {
                var item = XmlSerialization.DeserializeFromFile<TItem>(file);
                return item;
            }
            return default(TItem);
        }

        public virtual List<TItem> GetAll()
        {            
            var list = Directory.GetFiles(_folder, "*.xml").Select(item => XmlSerialization.DeserializeFromFile<TItem>(item)).ToList();
            return list;
        }

        public virtual void Delete(TIDType id)
        {
            var file = Path.Combine(_folder, _getFileFunction(id));
            if (File.Exists(file)) File.Delete(file);
        }

        public virtual void Add(TItem item)
        {
            var id = _getIdFunction(item);
            var file = Path.Combine(_folder, _getFileFunction(id));
            XmlSerialization.SerializeToFile(item, file, typeof(TItem));
        }

        public virtual void Update(TItem item)
        {
            var id = _getIdFunction(item);
            var file = Path.Combine(_folder, _getFileFunction(id));
            XmlSerialization.SerializeToFile(item, file, typeof(TItem));
        }
    }
}
