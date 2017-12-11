using GREEDY.Data;

namespace GREEDY.Extensions
{
    public static class DataModelsExtensions
    {
        public static bool HasValue(this ShopDataModel shop)
        {
            if (shop != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}