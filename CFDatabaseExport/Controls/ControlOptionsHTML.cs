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
    /// Control for exporting data to HTML
    /// </summary>
    public partial class ControlOptionsHTML : UserControl, IControlOptions
    {
        public QueryOptionsHTML QueryOptions { get; set; }

        public ControlOptionsHTML()
        {
            InitializeComponent();
        }

        public ControlOptionsHTML(QueryOptionsHTML queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsHTML queryOptions)
        {
            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
            txtOutputFile.Text = queryOptions.FileName;
            txtTemplateFile.Text = queryOptions.TemplateFile;
        }

        private void ViewToModel(QueryOptionsHTML queryOptions)
        {
            queryOptions.DateFormat = txtDateFormat.Text;
            queryOptions.NullString = txtNull.Text;
            queryOptions.FileName = txtOutputFile.Text;
            queryOptions.TemplateFile = txtTemplateFile.Text;
        }

        public List<string> ValidateModel()
        {
            var messages = new List<string>();
            if (String.IsNullOrEmpty(txtOutputFile.Text)) messages.Add("Output file is invalid or not set");
            if (String.IsNullOrEmpty(txtDateFormat.Text)) messages.Add("Date format is invalid or not set");
            if (String.IsNullOrEmpty(txtTemplateFile.Text) || !File.Exists(txtTemplateFile.Text)) messages.Add("Template file is invalid or not set");
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

        private void btnSelectTemplateFile_Click(object sender, EventArgs e)
        {
            txtTemplateFile.Text = UIUtilities.SelectFile("Template file", txtTemplateFile.Text, "HTML files|*.htm;*.html", true, true);
        }

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "HTML files|*.htm;*.html", false, false);
        }

        //public string DateFormat
        //{
        //    get { return txtDateFormat.Text; }
        //}

        //public string NullString
        //{
        //    get { return txtNull.Text; }
        //}
    }
}
