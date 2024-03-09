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
    /// Control for exporting data to SQL
    /// </summary>
    public partial class ControlOptionsSQL : UserControl, IControlOptions
    {
        public QueryOptionsSQL QueryOptions { get; set; }

        private readonly string _dateFormat;

        public ControlOptionsSQL()
        {           
            InitializeComponent();

            /*
            txtSQL.Text = "CREATE TABLE MyNewTable (Column1 INT, Column2 NVARCHAR(1000)) " + Environment.NewLine + Environment.NewLine +
                        "--The row template will be inserted below for each row that the query returns:" + Environment.NewLine +
                        "##ROW_TEMPLATES##" + Environment.NewLine + Environment.NewLine +
                        "SELECT COUNT(1) FROM MyNewTable";
            */

            //txtRowTemplateSQL.Text = "INSERT INTO MyNewTable(Column1, Column2) VALUES (##COLUMN_VALUE_1##, ##COLUMN_VALUE_2##) ";

            txtComments.Text = Comments;
        }

        private string Comments
        {
            get
            {
                return "You can use the following placeholders:" + Environment.NewLine + Environment.NewLine +
                            "##ROW_TEMPLATES## \t\t Row template for each row returned by the query" + Environment.NewLine +
                            "##COLUMN_NAME_[N]## \t\t Column name for Nth column." + Environment.NewLine +
                            "##COLUMN_NAME_[NAME]## \t Column name for named column." + Environment.NewLine +
                            "##COLUMN_VALUE_[N]## \t\t Column value for Nth column." + Environment.NewLine +
                            "##COLUMN_VALUE_[NAME]## \t Column value for named column." + Environment.NewLine;
            }
        }


        public ControlOptionsSQL(QueryOptionsSQL queryOptions)
        {
            InitializeComponent();

            this.QueryOptions = queryOptions;

            txtTemplateSQLFile.Text = queryOptions.TemplateSQLFile;
            txtRowTemplateSQL.Text = queryOptions.RowTemplateSQL;
            txtOutputFile.Text = queryOptions.FileName;

            txtComments.Text = Comments;
        }

        public bool CanApplyToModel()
        {
            return true;
        }

        public void ApplyToModel()
        {
            QueryOptions.TemplateSQLFile = txtTemplateSQLFile.Text;
            QueryOptions.RowTemplateSQL = txtRowTemplateSQL.Text;
            QueryOptions.DateFormat = this.QueryOptions.DateFormat;
            QueryOptions.NullString = "NULL";
            QueryOptions.FileName = txtOutputFile.Text;        
        }

        private void btnSelectTemplateSQLFile_Click(object sender, EventArgs e)
        {
            txtTemplateSQLFile.Text = UIUtilities.SelectFile("Select template SQL file", txtTemplateSQLFile.Text, "SQL files|*.sql", true, true);
        }

        private void btnSelectOutputFile_Click(object sender, EventArgs e)
        {
            txtOutputFile.Text = UIUtilities.SelectFile("Output file", txtOutputFile.Text, "SQL files|*.sql", false, false);
        }

        //public string TemplateSQL
        //{
        //    get { return txtSQL.Text; }
        //}

        //public string RowTemplateSQL
        //{
        //    get { return txtRowTemplateSQL.Text; }
        //}    

        //public string DateFormat
        //{
        //    get { return ApplicationObject.DateTimeFormat1; }
        //}

        //public string NullString
        //{
        //    get { return "NULL"; }
        //}        
    }
}
