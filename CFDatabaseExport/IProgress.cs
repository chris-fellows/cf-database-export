using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFUtilities;

namespace CFDatabaseExport
{
    public interface IProgress
    {
        void SetStatusMessage(Message message);
    }
}
