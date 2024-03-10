using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDatabaseExport.Exceptions;

namespace CFDatabaseExport.Controls
{
    /// <summary>
    /// Interface for control that allows edit of handling options
    /// </summary>
    interface IControlOptions
    {
        /// <summary>
        /// Applies changes to model. Raises error if invalid.
        /// </summary>
        /// <exception cref="HandleOptionsInvalidException"></exception>
        void ApplyToModel();

        /// <summary>
        /// Validates model in current view state. User may have modified options and made it invalid.
        /// </summary>
        /// <returns>Validation issues</returns>
        List<string> ValidateModel();
    }
}
