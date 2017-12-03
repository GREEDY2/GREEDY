using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.ReceiptCreatings
{
    public interface IItemCategorization
    {
        List<Item> CategorizeItems(List<Item> NewData);
        void AddCategory(string itemName, string category);
    }
}