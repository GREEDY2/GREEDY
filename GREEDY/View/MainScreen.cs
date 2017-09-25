using GREEDY.Controllers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using GREEDY.Models;

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
                string receiptsFolder = ConfigurationManager.AppSettings["receiptsFolder"];
                string singleReceiptPath = ConfigurationManager.AppSettings["singleReceiptPath"];
                var receipt = new OCRController().UseOCR(imageForOCR.FileName);
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
