using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFDatabaseExport.Models;
using CFDatabaseExport.Exceptions;

namespace CFDatabaseExport.Controls
{/// <summary>
 /// Control for exporting data to grid
 /// </summary>
    public partial class ControlOptionsGrid : UserControl, IControlOptions
    {
        public QueryOptionsGrid QueryOptions { get; set; }

        public ControlOptionsGrid()
        {
            InitializeComponent();
        }

        public ControlOptionsGrid(QueryOptionsGrid queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsGrid queryOptions)
        {
            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
        }

        private void ViewToModel(QueryOptionsGrid queryOptions)
        {
            queryOptions.DateFormat = txtDateFormat.Text;
            queryOptions.NullString = txtNull.Text;
        }

        public List<string> ValidateModel()
        {
            var messages = new List<string>();            
            if (String.IsNullOrEmpty(txtDateFormat.Text)) messages.Add("Date format is invalid or not set");
            return messages;
        }

        public void ApplyToModel()
        {
            if (ValidateModel().Any())
            {
                throw new HandleOptionsInvalidException("Cannot apply changes to model because they are invalid");
            }
            ViewToModel(QueryOptions);            
        }      
    }
}
