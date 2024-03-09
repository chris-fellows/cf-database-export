using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFDatabaseExport.Controls;

namespace CFDatabaseExport.Models
{
    /// <summary>
    /// Details for individual output format (CSV/HTML/Excel etc)
    /// </summary>
    public class OutputFormat
    {
        /// <summary>
        /// Display name
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// Comments for output format. E.g. Additional instructions.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Control for changing options
        /// </summary>
        public UserControl OptionsUserControl { get; set; }

        /// <summary>
        /// Model containing query options
        /// </summary>
        public QueryOptions QueryOptions { get; set; }

        public OutputFormat(string display, string comments, UserControl optionsUserControl, QueryOptions queryOptions)
        {
            Display = display;
            Comments = comments;
            OptionsUserControl = optionsUserControl;
            QueryOptions = queryOptions;
        }

        public void ApplyUserControlOptionsToModel()
        {
            if (OptionsUserControl != null)
            {
                IControlOptions controlOptions = (IControlOptions)OptionsUserControl;
                controlOptions.ApplyToModel();
            }
        }

        public bool CanApplyUserControlOptionsToModel()
        {
            if (OptionsUserControl != null)
            {
                IControlOptions controlOptions = (IControlOptions)OptionsUserControl;
                return controlOptions.CanApplyToModel();
            }
            return true;
        }
    }
}