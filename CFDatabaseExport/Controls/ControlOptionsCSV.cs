using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFDatabaseExport.Exceptions;
using CFDatabaseExport.Models;
using CFDatabaseExport.Utilities;
using CFUtilities;
using System.CodeDom;

namespace CFDatabaseExport.Controls
{
    /// <summary>
    /// Control for exporting data to CSV
    /// </summary>
    public partial class ControlOptionsCSV : UserControl, IControlOptions
    {
        public QueryOptionsCSV QueryOptions { get; set; }

        public ControlOptionsCSV()
        {
            InitializeComponent();
        }

        public ControlOptionsCSV(QueryOptionsCSV queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;
          
            List<NameValuePair<Char>> delimeters = new List<NameValuePair<Char>>();
            delimeters.Add(new NameValuePair<Char>("Comma (,)", ','));
            delimeters.Add(new NameValuePair<Char>("Tab", (Char)9));
            delimeters.Add(new NameValuePair<Char>("Semi-colon (;)", ';'));
            delimeters.Add(new NameValuePair<Char>("Pipe (|)", '|'));
            delimeters.Add(new NameValuePair<Char>("Hash (#)", '#'));
            delimeters.Add(new NameValuePair<Char>("Zero", (Char)0));

            cboDelimiter.DisplayMember = "Name";
            cboDelimiter.ValueMember = "Value";
            cboDelimiter.DataSource = delimeters;

            ModelToView(queryOptions);
        }

        private void ModelToView(QueryOptionsCSV queryOptions)
        {
            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
            txtOutputFile.Text = queryOptions.FileName;
            cboDelimiter.SelectedValue = queryOptions.Delimiter;
        }

        private void ViewToModel(QueryOptionsCSV queryOptions)
        {
            queryOptions.FileName = txtOutputFile.Text;
            queryOptions.DateFormat = txtDateFormat.Text;
            queryOptions.NullString = txtNull.Text;
            queryOptions.Delimiter = (Char)cboDelimiter.SelectedValue;
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
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "Delimited files|*.csv;*.txt", false, false);
        }

        private void txtOutputFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        //public string DateFormat
        //{
        //    get { return txtDateFormat.Text; }
        //}

        //public string NullString
        //{
        //    get { return txtNull.Text; }
        //}

        //public bool HeadersQuoted
        //{
        //    get { return chkHeadersQuoted.Checked; }
        //}

        //public bool ValuesQuoted
        //{
        //    get { return chkValuesQuoted.Checked; }
        //}
    }
}
