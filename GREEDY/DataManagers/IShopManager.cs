using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IShopManager
    {
        List<Shop> GetExistingShops();
        List<ShopData> GetAllUserShops(string username);
    }
}