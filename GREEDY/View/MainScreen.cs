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
                var receipt = new OCRController().UseOCR(imageForOCR.FileName);
                textResult.Text = string.Empty;
                foreach (var line in receipt.LinesOfText)
                {
                    textResult.Text += line;
                }
                new CreatePathForDataController().CreateAFolder("../../../Data/receipts");
                new WritingToFileController().WriteToFile("../../../Data/receipts/receipt2.txt", receipt);
                DataFormatController dataFormatController = new DataFormatController(receipt);
                ItemsList.DataSource = dataFormatController.GetDataTable();

            }
            GC.Collect();
        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemList_CellContentClick()
        {
            
        }
    }
}
