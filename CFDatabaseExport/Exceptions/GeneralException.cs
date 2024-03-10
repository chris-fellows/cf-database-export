using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Exceptions
{
    /// <summary>
    /// General CFDatabaseExport exception. Raise this exception where no specific exception exists
    /// </summary>    
    public class GeneralException : Exception
    {
        public GeneralException()
        {
        }

        public GeneralException(string message) : base(message)
        {

        }

        public GeneralException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public GeneralException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
