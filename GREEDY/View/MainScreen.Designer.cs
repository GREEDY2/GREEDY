namespace GREEDY
{
    partial class Greedy
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOCR = new System.Windows.Forms.Button();
            this.textResult = new System.Windows.Forms.TextBox();
            this.imageForOCR = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnOCR
            // 
            this.btnOCR.Location = new System.Drawing.Point(12, 12);
            this.btnOCR.Name = "btnOCR";
            this.btnOCR.Size = new System.Drawing.Size(135, 52);
            this.btnOCR.TabIndex = 0;
            this.btnOCR.Text = "OCR";
            this.btnOCR.UseVisualStyleBackColor = true;
            this.btnOCR.Click += new System.EventHandler(this.btnOCR_Click);
            // 
            // textResult
            // 
            this.textResult.Location = new System.Drawing.Point(12, 81);
            this.textResult.Multiline = true;
            this.textResult.Name = "textResult";
            this.textResult.Size = new System.Drawing.Size(853, 389);
            this.textResult.TabIndex = 1;
            this.textResult.TextChanged += new System.EventHandler(this.textResult_TextChanged);
            // 
            // Greedy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(877, 482);
            this.Controls.Add(this.textResult);
            this.Controls.Add(this.btnOCR);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Greedy";
            this.Text = "GREEDY";
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOCR;
        private System.Windows.Forms.TextBox textResult;
        private System.Windows.Forms.OpenFileDialog imageForOCR;
    }
}

