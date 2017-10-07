using System.Collections.Generic;
using System.Drawing;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IDataManager
    {
        // TODO: remove unnecessary methods
        void SaveData(List<Item> itemList);
        List<Item> LoadData();
        void DisplayToScreen(List<Item> items);
    }
}