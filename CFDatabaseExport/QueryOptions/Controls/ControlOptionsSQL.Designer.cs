namespace CFDatabaseExport.Controls
{
    partial class ControlOptionsSQL
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRowTemplateSQL = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.txtTemplateSQLFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectTemplateSQLFile = new System.Windows.Forms.Button();
            this.btnSelectOutputFile = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Row template SQL:";
            // 
            // txtRowTemplateSQL
            // 
            this.txtRowTemplateSQL.Location = new System.Drawing.Point(6, 211);
            this.txtRowTemplateSQL.Multiline = true;
            this.txtRowTemplateSQL.Name = "txtRowTemplateSQL";
            this.txtRowTemplateSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRowTemplateSQL.Size = new System.Drawing.Size(609, 90);
            this.txtRowTemplateSQL.TabIndex = 1;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(3, 13);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.Size = new System.Drawing.Size(612, 116);
            this.txtComments.TabIndex = 2;
            // 
            // txtTemplateSQLFile
            // 
            this.txtTemplateSQLFile.Location = new System.Drawing.Point(99, 152);
            this.txtTemplateSQLFile.Name = "txtTemplateSQLFile";
            this.txtTemplateSQLFile.Size = new System.Drawing.Size(516, 20);
            this.txtTemplateSQLFile.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "SQL template file:";
            // 
            // btnSelectTemplateSQLFile
            // 
            this.btnSelectTemplateSQLFile.Location = new System.Drawing.Point(619, 152);
            this.btnSelectTemplateSQLFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectTemplateSQLFile.Name = "btnSelectTemplateSQLFile";
            this.btnSelectTemplateSQLFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectTemplateSQLFile.TabIndex = 12;
            this.btnSelectTemplateSQLFile.Text = "...";
            this.btnSelectTemplateSQLFile.UseVisualStyleBackColor = true;
            this.btnSelectTemplateSQLFile.Click += new System.EventHandler(this.btnSelectTemplateSQLFile_Click);
            // 
            // btnSelectOutputFile
            // 
            this.btnSelectOutputFile.Location = new System.Drawing.Point(619, 317);
            this.btnSelectOutputFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectOutputFile.Name = "btnSelectOutputFile";
            this.btnSelectOutputFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectOutputFile.TabIndex = 15;
            this.btnSelectOutputFile.Text = "...";
            this.btnSelectOutputFile.UseVisualStyleBackColor = true;
            this.btnSelectOutputFile.Click += new System.EventHandler(this.btnSelectOutputFile_Click);
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(82, 319);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(533, 20);
            this.txtOutputFile.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Output file:";
            // 
            // ControlOptionsSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectOutputFile);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectTemplateSQLFile);
            this.Controls.Add(this.txtTemplateSQLFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.txtRowTemplateSQL);
            this.Controls.Add(this.label1);
            this.Name = "ControlOptionsSQL";
            this.Size = new System.Drawing.Size(644, 352);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRowTemplateSQL;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.TextBox txtTemplateSQLFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectTemplateSQLFile;
        private System.Windows.Forms.Button btnSelectOutputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label label2;
    }
}
