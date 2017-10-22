namespace GREEDY.View
{
    partial class ImageResizeTest
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
            this.originalPictureBox = new System.Windows.Forms.PictureBox();
            this.resizedPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resizedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // originalPictureBox
            // 
            this.originalPictureBox.Location = new System.Drawing.Point(13, 13);
            this.originalPictureBox.Name = "originalPictureBox";
            this.originalPictureBox.Size = new System.Drawing.Size(471, 547);
            this.originalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalPictureBox.TabIndex = 0;
            this.originalPictureBox.TabStop = false;
            // 
            // resizedPictureBox
            // 
            this.resizedPictureBox.Location = new System.Drawing.Point(515, 12);
            this.resizedPictureBox.Name = "resizedPictureBox";
            this.resizedPictureBox.Size = new System.Drawing.Size(471, 547);
            this.resizedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.resizedPictureBox.TabIndex = 1;
            this.resizedPictureBox.TabStop = false;
            // 
            // ImageResizeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 585);
            this.Controls.Add(this.resizedPictureBox);
            this.Controls.Add(this.originalPictureBox);
            this.Name = "ImageResizeTest";
            this.Text = "ImageResizeTest";
            this.Load += new System.EventHandler(this.ImageResizeTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.originalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resizedPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox originalPictureBox;
        private System.Windows.Forms.PictureBox resizedPictureBox;
    }
}