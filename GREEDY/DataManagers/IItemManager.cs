﻿using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IItemManager
    {
        int AddItems(IEnumerable<Item> itemList, Shop shop, string username);
        List<Item> GetItemsOfSingleReceipt(int receiptId);
        List<Item> GetAllUserItems(string username);
        List<Item> LoadData(string username);
        void UpdateItem(Item updatedItem);
    }
}