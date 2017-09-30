using System.Collections.Generic;
using GREEDY.Models;
using System.Windows.Forms;
using System.IO;

namespace GREEDY.DataManagers
{
    public class DataManager : IDataManager
    {
        private readonly SaveFileDialog _saveFileDialog;
        private readonly string _saveDataDialogTitle = "Save an Image File";

        public DataManager()
        {
            _saveFileDialog = new SaveFileDialog();
        }

        // not tested yet. Need to write a methods to create a file
        public void SaveData(List<Item> itemList)
        {
            using (_saveFileDialog)
            {
                _saveFileDialog.InitialDirectory = Environment.AppConfig.SaveFilePath;
                //saveFileDialog.Filter = _config.Filter;
                _saveFileDialog.Title = _saveDataDialogTitle;
                _saveFileDialog.RestoreDirectory = true;

                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //var SaveDataPath = saveFileDialog.FileName;
                    if (_saveFileDialog.FileName != "")
                    {
                        FileStream fs = (FileStream)_saveFileDialog.OpenFile();
                        fs.Close();
                    }
                    else
                    {
                        // what do you think about this part? how can I write exception if user close a window and do not select a picture/file?
                        // i dont know honestly
                    }
                }
            };
        }

        /// <summary>
        /// Gets data from {TBD}
        /// </summary>
        /// <returns></returns>
        public List<Item> LoadData()
        {
            throw new System.NotImplementedException();
        }
    }
}