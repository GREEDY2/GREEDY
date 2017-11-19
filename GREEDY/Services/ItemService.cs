using GREEDY.DataManagers;
using GREEDY.ReceiptCreatings;

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

        public void AddChangeCategory(string itemName, string category)
        {
            _itemCategorization.AddChangeCategories(itemName.ToLower(), category.ToLower());
        }
    }
}