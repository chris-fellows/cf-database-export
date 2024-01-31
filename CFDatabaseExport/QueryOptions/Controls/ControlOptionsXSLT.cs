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

            txtTransformFile.Text = queryOptions.TransformFile;
            txtOutputFile.Text = queryOptions.FileName;
        }

        public bool CanApplyToModel()
        {
            return true;
        }

        public void ApplyToModel()
        {
            QueryOptions.TransformFile = txtTransformFile.Text;
            QueryOptions.FileName = txtOutputFile.Text;
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
