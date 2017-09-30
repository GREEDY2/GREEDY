using System.Drawing;
using System.Windows.Forms;

namespace GREEDY.DataManagers
{
    class FileImageGetter : IImageGetter
    {
        private static IAppConfig AppConfig => new AppConfig();
        private OpenFileDialog GetPathDialog = new OpenFileDialog();

        public Bitmap GetImage()
        {
            using (GetPathDialog)
            {
                GetPathDialog.Filter = AppConfig.FilterImageFiles;
                GetPathDialog.FilterIndex = 2;
                GetPathDialog.RestoreDirectory = true;

                if (GetPathDialog.ShowDialog() == DialogResult.OK)
                {
                    if (GetPathDialog.FileName != "")
                    {
                        var imageBitmap = new Bitmap(GetPathDialog.FileName);
                        return imageBitmap;
                    }
                    else
                    {
                        // what do you think about this part? how can I write exception if user close a window and do not select a picture/file?
                        return null;
                    }
                }
                return null;
            };
        }
    }
}
