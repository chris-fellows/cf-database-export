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
        public string Display { get; set; }
        public string Comments { get; set; }
        public UserControl OptionsUserControl { get; set; }
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