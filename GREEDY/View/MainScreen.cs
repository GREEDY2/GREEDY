using GREEDY.Controllers;
using System;
using GREEDY.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using GREEDY.Extensions;
using Emgu.CV;
using Emgu.CV.Structure;

namespace GREEDY
{
    public partial class Greedy : Form
    {
        public Greedy()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }

        private void btnOCR_Click(object sender, EventArgs e)
        {
            if (imageForOCR.ShowDialog() == DialogResult.OK)
            {
                var receipt1 = new OCRController().UseOCR(new Bitmap(imageForOCR.FileName));
                Image<Bgr, byte> img = new Image<Bgr, byte>(imageForOCR.FileName);
                var receipt2 = new OCRController().UseOCR(img);
                textResult.Text = string.Empty;
                foreach (var line in receipt1.LinesOfText)
                {
                    textResult.Text += line;
                }
                new CreatePathForDataController("../../../Data/receipts").CreateAFolder();
                new WritingToFileController(receipt1, "../../../Data/receipts/receipt1.txt").WriteToFile();
                new WritingToFileController(receipt2, "../../../Data/receipts/receipt2.txt").WriteToFile();

            }
            GC.Collect();
        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
