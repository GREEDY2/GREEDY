namespace GREEDY.View
{
    partial class ImageViewTest
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
            this.receiptPictureBox = new System.Windows.Forms.PictureBox();
            this.textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.receiptPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // receiptPictureBox
            // 
            this.receiptPictureBox.Location = new System.Drawing.Point(12, 12);
            this.receiptPictureBox.Name = "receiptPictureBox";
            this.receiptPictureBox.Size = new System.Drawing.Size(338, 595);
            this.receiptPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.receiptPictureBox.TabIndex = 0;
            this.receiptPictureBox.TabStop = false;
            this.receiptPictureBox.Click += new System.EventHandler(this.receiptPictureBox_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(356, 12);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(350, 595);
            this.textBox.TabIndex = 1;
            // 
            // ImageViewTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 619);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.receiptPictureBox);
            this.Name = "ImageViewTest";
            this.Text = "ImageViewTest";
            this.Load += new System.EventHandler(this.ImageViewTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.receiptPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox receiptPictureBox;
        private System.Windows.Forms.TextBox textBox;
    }
}