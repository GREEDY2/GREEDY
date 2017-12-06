using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface IShopManager
    {
        List<Shop> GetExistingShops();
        List<ShopData> GetAllUserShops(string username);
    }
}