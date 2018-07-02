using System;
using GREEDY.ReceiptCreating;

namespace GREEDY.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemCategorization _itemCategorization;

        public ItemService(IItemCategorization itemCategorization)
        {
            _itemCategorization = itemCategorization;
        }

        public void AddCategory(string itemName, string category)
        {
            if (itemName != null && category != null)
                _itemCategorization.AddCategory(itemName, category);
            else
                throw new NullReferenceException();
        }
    }
}