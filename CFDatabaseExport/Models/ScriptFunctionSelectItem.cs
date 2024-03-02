using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport
{
    /// <summary>
    /// Script function to select one or more items from a list
    /// </summary>
    public class ScriptFunctionSelectItem : ScriptFunction
    {
        /// <summary>
        /// Prompt to display
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// SQL variable to store values for selected item(s)
        /// </summary>
        public string Variable { get; set; }

        /// <summary>
        /// Min number of items to select (0=No miniumum)
        /// </summary>
        public int MinItems { get; set; }

        /// <summary>
        /// Max number of items to select (0=No maximum)
        /// </summary>
        public int MaxItems { get; set; }
    }
}
