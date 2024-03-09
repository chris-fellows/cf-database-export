﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CFDatabaseExport.Models;
using CFUtilities.XML;

namespace CFDatabaseExport
{
    public class XmlDatabaseInfoService : IDatabaseInfoService
    {
        private string _folder;

        public XmlDatabaseInfoService(string folder)
        {
            _folder = folder;
        }

        public List<DatabaseInfo> GetAll()
        {
            string filename = Path.Combine(_folder, "Database Info.xml");
            ItemList<DatabaseInfo> databaseInfoList = XmlSerialization.DeserializeFromFile<ItemList<DatabaseInfo>>(filename);
            return databaseInfoList.Items;
        }
    }
}