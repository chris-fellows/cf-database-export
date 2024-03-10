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
    /// Control for exporting data to XML
    /// </summary>
    public partial class ControlOptionsXML : UserControl, IControlOptions
    {
        public QueryOptionsXML QueryOptions { get; set; }

        public ControlOptionsXML()
        {
            InitializeComponent();
        }

        public ControlOptionsXML(QueryOptionsXML queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsXML queryOptions)
        {
            txtOutputFile.Text = queryOptions.FileName;
        }

        private void ViewToModel(QueryOptionsXML queryOptions)
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
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "XML files|*.xml", false, false);
        }
    }
}
