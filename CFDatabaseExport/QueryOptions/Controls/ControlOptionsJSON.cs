using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFUtilities;

namespace CFDatabaseExport.Controls
{
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
