using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IItemManager
    {
        int AddItems(IEnumerable<Item> itemList, Shop shop, string Username);
        List<Item> GetItemsOfSingleReceipt(int receiptId);
        List<Item> LoadData(string Username);
        void UpdateItem(Item updatedItem);
    }
}