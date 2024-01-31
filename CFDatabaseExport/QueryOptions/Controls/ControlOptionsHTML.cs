using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFDatabaseExport.Controls
{
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

            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
            txtOutputFile.Text = queryOptions.FileName;
            txtTemplateFile.Text = queryOptions.TemplateFile;
        }

        public bool CanApplyToModel()
        {
            return true;
        }

        public void ApplyToModel()
        {
            QueryOptions.DateFormat = txtDateFormat.Text;
            QueryOptions.NullString = txtNull.Text;
            QueryOptions.FileName = txtOutputFile.Text;
            QueryOptions.TemplateFile = txtTemplateFile.Text;
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
