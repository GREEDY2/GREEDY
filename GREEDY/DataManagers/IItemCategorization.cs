namespace GREEDY.DataManagers
{
    public interface IItemCategorization
    {
        string CategorizeSingleItem(string itemName, decimal price);
        void AddItemToInfo(string itemName, string category);
        void AddChangeCategories();
    }
}