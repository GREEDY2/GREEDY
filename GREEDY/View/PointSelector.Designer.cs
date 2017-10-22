namespace GREEDY.View
{
    partial class PointSelector
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.point1x = new System.Windows.Forms.TextBox();
            this.point2x = new System.Windows.Forms.TextBox();
            this.point3y = new System.Windows.Forms.TextBox();
            this.point4y = new System.Windows.Forms.TextBox();
            this.goBtn = new System.Windows.Forms.Button();
            this.point1y = new System.Windows.Forms.TextBox();
            this.point2y = new System.Windows.Forms.TextBox();
            this.point3x = new System.Windows.Forms.TextBox();
            this.point4x = new System.Windows.Forms.TextBox();
            this.resetBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(118, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(462, 598);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // point1x
            // 
            this.point1x.Location = new System.Drawing.Point(12, 12);
            this.point1x.Name = "point1x";
            this.point1x.Size = new System.Drawing.Size(100, 26);
            this.point1x.TabIndex = 1;
            // 
            // point2x
            // 
            this.point2x.Location = new System.Drawing.Point(586, 12);
            this.point2x.Name = "point2x";
            this.point2x.Size = new System.Drawing.Size(100, 26);
            this.point2x.TabIndex = 2;
            // 
            // point3y
            // 
            this.point3y.Location = new System.Drawing.Point(12, 584);
            this.point3y.Name = "point3y";
            this.point3y.Size = new System.Drawing.Size(100, 26);
            this.point3y.TabIndex = 3;
            // 
            // point4y
            // 
            this.point4y.Location = new System.Drawing.Point(586, 584);
            this.point4y.Name = "point4y";
            this.point4y.Size = new System.Drawing.Size(100, 26);
            this.point4y.TabIndex = 4;
            // 
            // goBtn
            // 
            this.goBtn.Location = new System.Drawing.Point(586, 191);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(100, 93);
            this.goBtn.TabIndex = 5;
            this.goBtn.Text = "Go";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // point1y
            // 
            this.point1y.Location = new System.Drawing.Point(12, 44);
            this.point1y.Name = "point1y";
            this.point1y.Size = new System.Drawing.Size(100, 26);
            this.point1y.TabIndex = 6;
            // 
            // point2y
            // 
            this.point2y.Location = new System.Drawing.Point(586, 44);
            this.point2y.Name = "point2y";
            this.point2y.Size = new System.Drawing.Size(100, 26);
            this.point2y.TabIndex = 7;
            // 
            // point3x
            // 
            this.point3x.Location = new System.Drawing.Point(12, 552);
            this.point3x.Name = "point3x";
            this.point3x.Size = new System.Drawing.Size(100, 26);
            this.point3x.TabIndex = 8;
            // 
            // point4x
            // 
            this.point4x.Location = new System.Drawing.Point(586, 552);
            this.point4x.Name = "point4x";
            this.point4x.Size = new System.Drawing.Size(100, 26);
            this.point4x.TabIndex = 9;
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(586, 290);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(100, 93);
            this.resetBtn.TabIndex = 10;
            this.resetBtn.Text = "Reset points";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // PointSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 622);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.point4x);
            this.Controls.Add(this.point3x);
            this.Controls.Add(this.point2y);
            this.Controls.Add(this.point1y);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.point4y);
            this.Controls.Add(this.point3y);
            this.Controls.Add(this.point2x);
            this.Controls.Add(this.point1x);
            this.Controls.Add(this.pictureBox);
            this.Name = "PointSelector";
            this.Text = "pointSelector";
            this.Load += new System.EventHandler(this.pointSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox point1x;
        private System.Windows.Forms.TextBox point2x;
        private System.Windows.Forms.TextBox point3y;
        private System.Windows.Forms.TextBox point4y;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.TextBox point1y;
        private System.Windows.Forms.TextBox point2y;
        private System.Windows.Forms.TextBox point3x;
        private System.Windows.Forms.TextBox point4x;
        private System.Windows.Forms.Button resetBtn;
    }
}