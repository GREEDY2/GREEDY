using GREEDY.DataManagers;

namespace GREEDY.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemCategorization _itemCategorization;

        public ItemService(IItemCategorization itemCategorization)
        {
            _itemCategorization = itemCategorization;
        }

        public void AddChangeCategory(string itemName, string category)
        {
            _itemCategorization.AddChangeCategories(itemName.ToLower(), category.ToLower());
        }
    }
}