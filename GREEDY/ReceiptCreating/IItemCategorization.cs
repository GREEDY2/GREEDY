using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface IItemCategorization
    {
        //string CategorizeSingleItem(string itemName, decimal price);
        List<ItemInfo> CategorizeAllItems(List<ItemInfo> NewData);
        void AddChangeCategories();
    }
}