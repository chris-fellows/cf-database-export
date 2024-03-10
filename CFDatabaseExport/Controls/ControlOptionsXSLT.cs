using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
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
    /// Control for exporting data to XSLS generated content
    /// </summary>
    public partial class ControlOptionsXSLT : UserControl, IControlOptions
    {
        public QueryOptionsXSLT QueryOptions { get; set; }

        public ControlOptionsXSLT()
        {
            InitializeComponent();
        }

        public ControlOptionsXSLT(QueryOptionsXSLT queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsXSLT queryOptions)
        {
            txtTransformFile.Text = queryOptions.TransformFile;
            txtOutputFile.Text = queryOptions.FileName;
        }

        private void ViewToModel(QueryOptionsXSLT queryOptions)
        {
            queryOptions.TransformFile = txtTransformFile.Text;
            queryOptions.FileName = txtOutputFile.Text;
        }

        public List<string> ValidateModel()
        {
            var messages = new List<string>();
            if (String.IsNullOrEmpty(txtOutputFile.Text)) messages.Add("Output file is invalid or not set");
            if (String.IsNullOrEmpty(txtTransformFile.Text) || !File.Exists(txtTransformFile.Text)) messages.Add("Transform file is invalid or not set");
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

        private void btnSelectTransformFile_Click(object sender, EventArgs e)
        {
            txtTransformFile.Text = UIUtilities.SelectFile("Select transform file", txtTransformFile.Text, "XSLT files|*.xslt", true, true);
        }

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "", false, false);
        }
    }
}
