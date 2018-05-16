using GREEDY.DataManagers;
using GREEDY.ReceiptCreatings;
using System;

namespace GREEDY.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemCategorization _itemCategorization;

        public ItemService()
        {
            _itemCategorization = new ItemCategorization();
        }

        public ItemService(
            IDataConverter dataConverter, IItemManager dataManager, IItemCategorization itemCategorization)
        {
            _itemCategorization = itemCategorization;
        }

        public void AddCategory(string itemName, string category)
        {
            if (itemName != null && category != null)
            {
                _itemCategorization.AddCategory(itemName, category);
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}