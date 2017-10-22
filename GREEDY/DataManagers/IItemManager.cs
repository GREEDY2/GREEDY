using System.Collections.Generic;
using System.Drawing;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IItemManager
    {
        // TODO: remove unnecessary methods
        void AddItems(IEnumerable<Item> itemList, Shop shop, string Username);
        List<Item> LoadData(string Username);
    }
}