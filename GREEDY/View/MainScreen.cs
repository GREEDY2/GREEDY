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
                var receipt = new OCRController().UseOCR(new Bitmap(imageForOCR.FileName));
                textResult.Text = string.Empty;
                foreach (var line in receipt.LinesOfText)
                {
                    textResult.Text += line;
                }
                new CreatePathForDataController("../../../Data/receipts").CreateAFolder();
                new WritingToFileController(receipt, "../../../Data/receipts/receipt.txt").WriteToFile();
            }
            GC.Collect();
        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
