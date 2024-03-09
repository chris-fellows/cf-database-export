namespace CFDatabaseExport.Controls
{
    partial class ControlOptionsHTML
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
            this.txtDateFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNull = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTemplateFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectTemplateFile = new System.Windows.Forms.Button();
            this.btnSelectOutputFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDateFormat
            // 
            this.txtDateFormat.Location = new System.Drawing.Point(77, 11);
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Size = new System.Drawing.Size(206, 20);
            this.txtDateFormat.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date format:";
            // 
            // txtNull
            // 
            this.txtNull.Location = new System.Drawing.Point(79, 37);
            this.txtNull.Name = "txtNull";
            this.txtNull.Size = new System.Drawing.Size(204, 20);
            this.txtNull.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Null value:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(79, 63);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(517, 20);
            this.txtOutputFile.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Output file:";
            // 
            // txtTemplateFile
            // 
            this.txtTemplateFile.Location = new System.Drawing.Point(79, 92);
            this.txtTemplateFile.Name = "txtTemplateFile";
            this.txtTemplateFile.Size = new System.Drawing.Size(517, 20);
            this.txtTemplateFile.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Template file:";
            // 
            // btnSelectTemplateFile
            // 
            this.btnSelectTemplateFile.Location = new System.Drawing.Point(600, 90);
            this.btnSelectTemplateFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectTemplateFile.Name = "btnSelectTemplateFile";
            this.btnSelectTemplateFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectTemplateFile.TabIndex = 12;
            this.btnSelectTemplateFile.Text = "...";
            this.btnSelectTemplateFile.UseVisualStyleBackColor = true;
            this.btnSelectTemplateFile.Click += new System.EventHandler(this.btnSelectTemplateFile_Click);
            // 
            // btnSelectOutputFile
            // 
            this.btnSelectOutputFile.Location = new System.Drawing.Point(600, 61);
            this.btnSelectOutputFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectOutputFile.Name = "btnSelectOutputFile";
            this.btnSelectOutputFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectOutputFile.TabIndex = 13;
            this.btnSelectOutputFile.Text = "...";
            this.btnSelectOutputFile.UseVisualStyleBackColor = true;
            this.btnSelectOutputFile.Click += new System.EventHandler(this.btnSelectOutputFile_Click);
            // 
            // ControlOptionsHTML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectOutputFile);
            this.Controls.Add(this.btnSelectTemplateFile);
            this.Controls.Add(this.txtTemplateFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNull);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateFormat);
            this.Controls.Add(this.label1);
            this.Name = "ControlOptionsHTML";
            this.Size = new System.Drawing.Size(637, 127);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNull;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTemplateFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectTemplateFile;
        private System.Windows.Forms.Button btnSelectOutputFile;
    }
}
