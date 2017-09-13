using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

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
                var img = new Bitmap(imageForOCR.FileName);
                var ocr = new TesseractEngine("../../../Data/tessdata", "eng", EngineMode.TesseractAndCube);
                var page = ocr.Process(img);
                textResult.Text = page.GetText();
                GC.Collect();

            }
        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
