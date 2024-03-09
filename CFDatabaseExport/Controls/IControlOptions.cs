using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Controls
{
    interface IControlOptions
    {
        void ApplyToModel();
        bool CanApplyToModel();
    }
}
