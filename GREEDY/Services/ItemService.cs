using GREEDY.DataManagers;

namespace GREEDY.Services
{
    public class ItemService : IItemService
    {
        private readonly IDataConverter _dataConverter;
        private readonly IItemManager _dataManager;
        private readonly IItemCategorization _itemCategorization;

        public ItemService()
        {
            _dataConverter = new DataConverter();
            _dataManager = new ItemManager();
            _itemCategorization = new ItemCategorization();
        }

        public ItemService(
            IDataConverter dataConverter, IItemManager dataManager, IItemCategorization itemCategorization)
        {
            _dataConverter = dataConverter;
            _dataManager = dataManager;
            _itemCategorization = itemCategorization;
        }

        public void AddChangeCategory(string itemName, string category)
        {
            _itemCategorization.AddChangeCategories(itemName.ToLower(), category.ToLower());
        }
    }
}