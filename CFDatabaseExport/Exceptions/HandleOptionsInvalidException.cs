using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFDatabaseExport.Exceptions
{
    /// <summary>
    /// Exception for query handling options invalid. User has changed the options and they are currently
    /// invalid. E.g. User has selected a template file that does not exist.
    /// </summary>
    public class HandleOptionsInvalidException : Exception
    {
        public HandleOptionsInvalidException()
        {
        }

        public HandleOptionsInvalidException(string message) : base(message)
        {

        }

        public HandleOptionsInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public HandleOptionsInvalidException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
