namespace CFDatabaseExport.Controls
{
    partial class ControlOptionsJSON
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
            this.btnSelectOutputFile = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNull = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDateFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectOutputFile
            // 
            this.btnSelectOutputFile.Location = new System.Drawing.Point(631, 59);
            this.btnSelectOutputFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectOutputFile.Name = "btnSelectOutputFile";
            this.btnSelectOutputFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectOutputFile.TabIndex = 15;
            this.btnSelectOutputFile.Text = "...";
            this.btnSelectOutputFile.UseVisualStyleBackColor = true;
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(87, 61);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(540, 20);
            this.txtOutputFile.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Output file:";
            // 
            // txtNull
            // 
            this.txtNull.Location = new System.Drawing.Point(87, 35);
            this.txtNull.Name = "txtNull";
            this.txtNull.Size = new System.Drawing.Size(198, 20);
            this.txtNull.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Null value:";
            // 
            // txtDateFormat
            // 
            this.txtDateFormat.Location = new System.Drawing.Point(87, 9);
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Size = new System.Drawing.Size(198, 20);
            this.txtDateFormat.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Date format:";
            // 
            // ControlOptionsJSON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectOutputFile);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNull);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateFormat);
            this.Controls.Add(this.label1);
            this.Name = "ControlOptionsJSON";
            this.Size = new System.Drawing.Size(662, 94);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectOutputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNull;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Label label1;
    }
}
