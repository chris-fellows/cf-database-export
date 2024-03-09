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

namespace CFDatabaseExport.Controls
{/// <summary>
 /// Control for exporting data to grid
 /// </summary>
    public partial class ControlOptionsGrid : UserControl, IControlOptions
    {
        public QueryOptionsGrid QueryOptions { get; set; }

        public ControlOptionsGrid()
        {
            InitializeComponent();
        }

        public ControlOptionsGrid(QueryOptionsGrid queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            txtDateFormat.Text = queryOptions.DateFormat;
            txtNull.Text = queryOptions.NullString;
        }

        public bool CanApplyToModel()
        {
            return true;
        }

        public void ApplyToModel()
        {
            QueryOptions.DateFormat = txtDateFormat.Text;
            QueryOptions.NullString = txtNull.Text;
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
