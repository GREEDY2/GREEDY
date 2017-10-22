using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GREEDY.Services;

namespace GREEDY.View
{
    /// <inheritdoc />
    /// <summary>
    /// Temporary class to test image filters (Blur, Dilation, Erosion)
    /// </summary>
    public partial class ImageViewTest : Form
    {
        private readonly Bitmap _imageBitmap;
        private readonly string _name;
        private readonly IReceiptService _receiptService;
        public ImageViewTest(Bitmap imageBitmap, string name, IReceiptService receiptService)
        {
            InitializeComponent();
            _imageBitmap = imageBitmap;
            _name = name;
            _receiptService = receiptService;
        }

        private void ImageViewTest_Load(object sender, EventArgs e)
        {
            receiptPictureBox.Image = _imageBitmap;
            this.Text = _name;
            foreach(string line in _receiptService.TempProcessReceiptImage(_imageBitmap))
            {
                textBox.Text += line;
                textBox.Text += "\n";
            }
        }

        private void receiptPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
