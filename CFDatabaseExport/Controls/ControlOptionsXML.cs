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

            txtOutputFile.Text = queryOptions.FileName;
        }

        public bool CanApplyToModel()
        {
            return true;
        }

        public void ApplyToModel()
        {
            QueryOptions.FileName = txtOutputFile.Text;
        }

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "XML files|*.xml", false, false);
        }
    }
}
