using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.DataManagers
{
    interface IShopManager
    {
        List<Shop> GetExistingShop();
    }
}
