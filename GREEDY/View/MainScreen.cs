using System;
using System.Windows.Forms;
using GREEDY.Services;
using GREEDY.DataManagers;
using System.Linq;

namespace GREEDY.View
{
    public partial class MainScreen : Form
    {
        private readonly IReceiptService _receiptService;
        private readonly IImageGetter _photoImageGetter;
        private readonly IImageGetter _fileImageGetter;

        public MainScreen(ReceiptService receiptService)
        {
            _receiptService = receiptService;
            _photoImageGetter = new PhotoImageGetter();
            _fileImageGetter = new FileImageGetter();
            InitializeComponent();
        }

        private void InserFile_Button_Click(object sender, EventArgs e)
        {
            //DataGridViewComboBoxCell cmbCol = new DataGridViewComboBoxCell();
            /*cmbCol.
            cmbCol.HeaderText = "yourColumn";
            cmbCol.Name = "myComboColumn";
            cmbCol.Items.Add("True");*/

            //if want to add the fix value in ComboBox Male and Female

            

            var image = _fileImageGetter.GetImage();
            Application.UseWaitCursor = true;
            InserFile_Button.Enabled = false;
            var processedReceipt = _receiptService.ProcessReceiptImage(image);
            ItemList.DataSource = processedReceipt;
            //ItemList.Columns["Category"].Visible = false;

            DataGridViewComboBoxCell bc = new DataGridViewComboBoxCell();
            var ss = processedReceipt.Select(x => x.Category).Distinct();
            foreach (var item in ss)
            {
                bc.Items.AddRange(item);
            }
            DataGridViewColumn cc = new DataGridViewColumn(bc);
            /*var ss = processedReceipt.AsEnumerable()
                .Select(_ => _.Field<string>("gender")).
                .Distinct();
            bc.Items.AddRange(ss.ToArray());*/

            ItemList.Columns.Add(cc);
            Application.UseWaitCursor = false;
            InserFile_Button.Enabled = true;
        }

        private void PictureFromCamera_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var image = _photoImageGetter.GetImage();
                Application.UseWaitCursor = true;
                InserFile_Button.Enabled = false;
                var processedReceipt = _receiptService.ProcessReceiptImage(image);
                ItemList.DataSource = processedReceipt;
                Application.UseWaitCursor = false;
                InserFile_Button.Enabled = true;
            }
            catch (NotImplementedException ex)
            {
                WarningBox_MessageBox(ex.Message, "Perspėjimas");
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

        private void XMLdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void PictureBox1_Click(object sender, EventArgs e) { }
    }
}
