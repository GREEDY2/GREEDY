using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.ReceiptCreatings
{
    public interface IItemCategorization
    {
        List<ItemInfo> CategorizeAllItems(List<ItemInfo> NewData);
        void WriteCategories();
    }
}