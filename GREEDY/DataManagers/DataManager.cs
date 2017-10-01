﻿using System.Collections.Generic;
using GREEDY.Models;
using System.Windows.Forms;
using System.IO;

namespace GREEDY.DataManagers
{
    public class DataManager : IDataManager
    {
        private readonly SaveFileDialog _saveFileDialog;
        private readonly DataGridView _getDataGridWiew;
        private readonly string _saveDataDialogTitle = "Save an Image File";

        public DataManager()
        {
            _saveFileDialog = new SaveFileDialog();
            _getDataGridWiew = new DataGridView();
        }

        // not tested yet. Need to write a methods to create a file
        public void SaveData(List<Item> itemList)
        {
            using (_saveFileDialog)
            {
                _saveFileDialog.InitialDirectory = Environments.AppConfig.SaveFilePath;
                //_saveFileDialog.Filter = Environments.AppConfig.Filter;
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
                        // how can I write exception if user close a window and do not select a picture/file?
                    }
                }
            };
        }
        /// <summary>
        /// Get list and display to the screen a table. Will be usefull in the future
        /// </summary>
        /// <returns></returns>
        public void DisplayToScreen(List<Item> items)
        {
            _getDataGridWiew.DataSource = items;
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