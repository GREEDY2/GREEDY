using System.Drawing;
using System.Windows.Forms;

namespace GREEDY.DataManagers
{
    class FileImageGetter : IImageGetter
    {
        private readonly OpenFileDialog _getPathDialog;

        public FileImageGetter()
        {
            _getPathDialog = new OpenFileDialog
            {
                Filter = Environments.AppConfig.FilterImageFiles,
                FilterIndex = 2,
                RestoreDirectory = true
            };
        }

        public Bitmap GetImage()
        {
            if (_getPathDialog.ShowDialog() == DialogResult.OK)
            {
                if (_getPathDialog.FileName != "")
                {
                    var imageBitmap = new Bitmap(_getPathDialog.FileName);
                    return imageBitmap;
                }
            }
            // what would a using acoomplish here?
            return null;
        }
    }
}
