using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
{
    public interface IItemCategorization
    {
        List<Item> CategorizeItems(List<Item> NewData);
        void AddCategory(string itemName, string category);
    }
}