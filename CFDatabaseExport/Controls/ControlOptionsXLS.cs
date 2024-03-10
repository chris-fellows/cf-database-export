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
using CFDatabaseExport.Utilities;
using CFDatabaseExport.Exceptions;

namespace CFDatabaseExport.Controls
{
    /// <summary>
    /// Control for exporting data to Excel (XLS)
    /// </summary>
    public partial class ControlOptionsXLS : UserControl, IControlOptions
    {
        public QueryOptionsXLS QueryOptions { get; set; }

        public ControlOptionsXLS()
        {
            InitializeComponent();
        }

        public ControlOptionsXLS(QueryOptionsXLS queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsXLS queryOptions)
        {
            txtOutputFile.Text = queryOptions.FileName;
        }

        private void ViewToModel(QueryOptionsXLS queryOptions)
        {
            queryOptions.FileName = txtOutputFile.Text;
        }

        public List<string> ValidateModel()
        {
            var messages = new List<string>();
            if (String.IsNullOrEmpty(txtOutputFile.Text)) messages.Add("Output file is invalid or not set");            
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

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "Delimited files|*.csv;*.txt", false, false);
        }
    }
}
