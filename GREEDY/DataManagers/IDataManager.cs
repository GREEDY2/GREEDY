using System.Collections.Generic;
using System.Drawing;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IDataManager
    {
        void SaveData (List<Item> itemList);
        List<Item> LoadData ();
    }
}