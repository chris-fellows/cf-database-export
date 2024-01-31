namespace CFDatabaseExport.Controls
{
    partial class ControlOptionsXSLT
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
            this.txtTransformFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectTransformFile = new System.Windows.Forms.Button();
            this.btnSelectOutputFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTransformFile
            // 
            this.txtTransformFile.Location = new System.Drawing.Point(84, 12);
            this.txtTransformFile.Name = "txtTransformFile";
            this.txtTransformFile.Size = new System.Drawing.Size(535, 20);
            this.txtTransformFile.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Transform file:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(84, 42);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(535, 20);
            this.txtOutputFile.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Output file:";
            // 
            // btnSelectTransformFile
            // 
            this.btnSelectTransformFile.Location = new System.Drawing.Point(623, 10);
            this.btnSelectTransformFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectTransformFile.Name = "btnSelectTransformFile";
            this.btnSelectTransformFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectTransformFile.TabIndex = 10;
            this.btnSelectTransformFile.Text = "...";
            this.btnSelectTransformFile.UseVisualStyleBackColor = true;
            this.btnSelectTransformFile.Click += new System.EventHandler(this.btnSelectTransformFile_Click);
            // 
            // btnSelectOutputFile
            // 
            this.btnSelectOutputFile.Location = new System.Drawing.Point(623, 39);
            this.btnSelectOutputFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelectOutputFile.Name = "btnSelectOutputFile";
            this.btnSelectOutputFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectOutputFile.TabIndex = 11;
            this.btnSelectOutputFile.Text = "...";
            this.btnSelectOutputFile.UseVisualStyleBackColor = true;
            this.btnSelectOutputFile.Click += new System.EventHandler(this.btnSelectOutputFile_Click);
            // 
            // ControlOptionsXSLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectOutputFile);
            this.Controls.Add(this.btnSelectTransformFile);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTransformFile);
            this.Controls.Add(this.label2);
            this.Name = "ControlOptionsXSLT";
            this.Size = new System.Drawing.Size(658, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTransformFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectTransformFile;
        private System.Windows.Forms.Button btnSelectOutputFile;
    }
}
