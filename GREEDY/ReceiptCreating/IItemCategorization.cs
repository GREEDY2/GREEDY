namespace GREEDY.ReceiptCreatings
{
    public interface IItemCategorization
    {
        string CategorizeSingleItem(string itemName, decimal price);
        void AddChangeCategories(string itemName, string category);
    }
}