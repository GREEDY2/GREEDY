﻿using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IItemManager
    {
        int AddItems(Receipt receipt, string username);
        List<Item> GetItemsOfSingleReceipt(int receiptId);
        List<Item> GetAllUserItems(string username);
        void UpdateItem(Item updatedItem);
        void DeleteItem(int itemId);
        void AddItem(Item newItem, int receiptId);
    }
}