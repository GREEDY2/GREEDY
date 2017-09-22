using GREEDY.Controllers;
using System;
using System.Windows.Forms;
using System.Configuration;

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

        private async void btnOCR_Click(object sender, EventArgs e)
        {
            if (imageForOCR.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Application.UseWaitCursor = true;
                    btnOCR.Enabled = false;
                    string receiptsFolder = ConfigurationManager.AppSettings["receiptsFolder"];
                    string singleReceiptPath = ConfigurationManager.AppSettings["singleReceiptPath"];
                    var receipt = await new OCRController().UseOCRAsync(imageForOCR.FileName);
                    textResult.Text = string.Empty;
                    foreach (var line in receipt.LinesOfText)
                    {
                        textResult.Text += line;
                    }
                    new CreatePathForDataController().CreateAFolder(receiptsFolder);
                    new WritingToFileController().WriteToFile(singleReceiptPath, receipt);
                    DataFormatController dataFormatController = new DataFormatController(receipt);
                    ItemsList.DataSource = dataFormatController.GetDataTable();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    Application.UseWaitCursor = false;
                    btnOCR.Enabled = true;
                }
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
