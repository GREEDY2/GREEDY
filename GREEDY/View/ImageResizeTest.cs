using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GREEDY.View
{
    public partial class ImageResizeTest : Form
    {
        private readonly Bitmap _originalBitmap;
        private readonly Bitmap _resizedBitmap;
        public ImageResizeTest(Bitmap originalBitmap, Bitmap resizedBitmap)
        {
            _originalBitmap = originalBitmap;
            _resizedBitmap = resizedBitmap;
            InitializeComponent();
        }

        private void ImageResizeTest_Load(object sender, EventArgs e)
        {
            originalPictureBox.Image = _originalBitmap;
            resizedPictureBox.Image = _resizedBitmap;
        }
    }
}
