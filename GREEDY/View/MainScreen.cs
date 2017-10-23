using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GREEDY.Services;
using GREEDY.DataManagers;
using System.Linq;

namespace GREEDY.View
{
    public partial class MainScreen : Form
    {
        private readonly IReceiptService _receiptService;
        private readonly IItemService _itemService;
        private readonly IImageGetter _photoImageGetter;
        private readonly IImageGetter _fileImageGetter;
        private readonly IImageFormating _imageFormatService;

        public MainScreen(IReceiptService receiptService, IItemService itemService)
        {
            _receiptService = receiptService;
            _itemService = itemService;
            _photoImageGetter = new PhotoImageGetter();
            _fileImageGetter = new FileImageGetter();
            InitializeComponent();
            _imageFormatService = new ImageFormating();
        }

        private void InserFile_Button_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            InserFile_Button.Enabled = false;
            var image = _fileImageGetter.GetImage();

            try
            {
                var processedReceipt = _receiptService.ProcessReceiptImage(image);
                if (processedReceipt != null)
                {
                    ItemList.DataSource = processedReceipt;
                    ItemList.Columns[0].ReadOnly = true;
                    ItemList.Columns[1].ReadOnly = true;
                }
            }
            catch (Exception)
            {
                WarningBox_MessageBox("Could not recognize how to process the receipt","Error");
            }
            
            Application.UseWaitCursor = false;
            InserFile_Button.Enabled = true;
        }

        private void ItemList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (ItemList.SelectedCells[0].Value != null)
            {
                _itemService.AddChangeCategory(
                    ItemList.CurrentRow.Cells[0].Value.ToString(), 
                    ItemList.SelectedCells[0].Value.ToString()
                );
            }
        }

        private void PictureFromCamera_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Application.UseWaitCursor = true;
                InserFile_Button.Enabled = false;
                var image = _photoImageGetter.GetImage();
                var processedReceipt = _receiptService.ProcessReceiptImage(image);
                ItemList.DataSource = processedReceipt;
            }
            catch (NotImplementedException ex)
            {
                WarningBox_MessageBox(ex.Message, "Perspėjimas");
            }
            finally
            {
                Application.UseWaitCursor = false;
                InserFile_Button.Enabled = true;
            }
            
        }

        private void WarningBox_MessageBox (string message, string windowTitle)
        {
            MessageBox.Show(
                 message,            //Message box text
                 windowTitle,            //Message box title
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Exclamation //For triangle Warning 
            );
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
