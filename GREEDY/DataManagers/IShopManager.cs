using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface IShopManager
    {
        List<Shop> GetExistingShops();
        List<Shop> GetAllUserShops(string username);
    }
}