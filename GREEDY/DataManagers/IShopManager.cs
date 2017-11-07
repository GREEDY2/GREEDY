using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    public interface IShopManager
    {
        List<Shop> GetExistingShop();
    }
}