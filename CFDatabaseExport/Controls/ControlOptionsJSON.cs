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
using CFUtilities;
using CFDatabaseExport.Exceptions;

namespace CFDatabaseExport.Controls
{
    /// <summary>
    /// Control for exporting data to JSON
    /// </summary>
    public partial class ControlOptionsJSON : UserControl, IControlOptions
    {
        public QueryOptionsJSON QueryOptions { get; set; }

        public ControlOptionsJSON()
        {
            InitializeComponent();
        }

        public ControlOptionsJSON(QueryOptionsJSON queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;
         
            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsJSON queryOptions)
        {
            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
            txtOutputFile.Text = queryOptions.FileName;
        }

        private void ViewToModel(QueryOptionsJSON queryOptions)
        {
            queryOptions.FileName = txtOutputFile.Text;
            queryOptions.DateFormat = txtDateFormat.Text;
            queryOptions.NullString = txtNull.Text;
        }

        public List<string> ValidateModel()
        {
            var messages = new List<string>();
            if (String.IsNullOrEmpty(txtOutputFile.Text)) messages.Add("Output file is invalid or not set");
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

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "JSON files|*.json", false, false);
        }

        private void txtOutputFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
