using GREEDY.Controllers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using GREEDY.Models;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;

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

                //writing raw data from receipt to items datatable 
                RawDataFormatController rawDataFormatController = new RawDataFormatController(receipt);
                ItemsList.DataSource = rawDataFormatController.GetDataTable();

                //reading list of all items and adding new items from receipt list
                string itemsListsFolder = ConfigurationManager.AppSettings["itemsListsFolder"];
                string singleItemsListPath = ConfigurationManager.AppSettings["singleItemsListPath"];
                new CreatePathForDataController().CreateAFolder(itemsListsFolder);
                DataConvertionController dataConvertionController = 
                    new DataConvertionController();

                //reading all items from an xml file
                List<Item> listOfAllItems = 
                    dataConvertionController.XmlToList(singleItemsListPath);

                //retrieving new receipts data and displaying it to DataGridView
                DataTable newReceipDataTable = 
                    new RawDataFormatController(receipt).GetDataTable();
                ItemsList.DataSource = newReceipDataTable;

                //converting new receipts datatable to List<Item>
                List<Item> newReceipItems = 
                    dataConvertionController.DataTableToList(newReceipDataTable);

                //adding new items to the listOfAllItems
                listOfAllItems.AddRange(newReceipItems);

                //writing the listOfAllItems to the xml file
                dataConvertionController.ListToXml(listOfAllItems, singleItemsListPath);

                Application.UseWaitCursor = false;
                btnOCR.Enabled = true;
            }
            GC.Collect();
        }

        private void textResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemList_CellContentClick()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageViewer viewer = new ImageViewer(); 
            VideoCapture capture = new VideoCapture(); 
            Application.Idle += new EventHandler(delegate (object senderis, EventArgs e1)
            {  
                viewer.Image = capture.QueryFrame(); 
            });
            viewer.ShowDialog(); 
            viewer.Image.Save(ConfigurationManager.AppSettings["imagePath"]);
            capture.Dispose();
        }
    }
}
