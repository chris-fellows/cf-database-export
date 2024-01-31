namespace CFDatabaseExport.Controls
{
    partial class ListItemsUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvwList = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwList
            // 
            this.lvwList.CheckBoxes = true;
            this.lvwList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvwList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwList.Location = new System.Drawing.Point(0, 0);
            this.lvwList.Name = "lvwList";
            this.lvwList.Size = new System.Drawing.Size(701, 463);
            this.lvwList.TabIndex = 9;
            this.lvwList.UseCompatibleStateImageBehavior = false;
            this.lvwList.View = System.Windows.Forms.View.Details;
            // 
            // ListItemUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvwList);
            this.Name = "ListItemUserControl";
            this.Size = new System.Drawing.Size(701, 463);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
