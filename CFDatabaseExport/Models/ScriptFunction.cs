using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Function that is referenced in a script
    /// </summary>
    public abstract class ScriptFunction
    {
        public string FunctionName { get; set; }
        public string ScriptText { get; set; }
    }
}
