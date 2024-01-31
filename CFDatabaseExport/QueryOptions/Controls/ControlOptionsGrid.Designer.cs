namespace CFDatabaseExport.Controls
{
    partial class ControlOptionsGrid
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
            this.SuspendLayout();
            // 
            // txtDateFormat
            // 
            this.txtDateFormat.Location = new System.Drawing.Point(88, 19);
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Size = new System.Drawing.Size(198, 20);
            this.txtDateFormat.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date format:";
            // 
            // txtNull
            // 
            this.txtNull.Location = new System.Drawing.Point(88, 50);
            this.txtNull.Name = "txtNull";
            this.txtNull.Size = new System.Drawing.Size(198, 20);
            this.txtNull.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Null value:";
            // 
            // ControlOptionsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtNull);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateFormat);
            this.Controls.Add(this.label1);
            this.Name = "ControlOptionsGrid";
            this.Size = new System.Drawing.Size(300, 85);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNull;
        private System.Windows.Forms.Label label2;
    }
}
