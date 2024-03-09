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

            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
            txtOutputFile.Text = queryOptions.FileName;

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
            cboDelimiter.SelectedValue = queryOptions.Delimiter;
        }

        public bool CanApplyToModel()
        {
            return true;
        }

        public void ApplyToModel()
        {
            QueryOptions.FileName = txtOutputFile.Text;
            QueryOptions.DateFormat = txtDateFormat.Text;
            QueryOptions.NullString = txtNull.Text;
            QueryOptions.Delimiter = (Char)cboDelimiter.SelectedValue;
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
