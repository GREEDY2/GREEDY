using GREEDY.Data;
using GREEDY.Models;
using System.Collections.Generic;
using System.Linq;

namespace GREEDY.DataManagers
{
    class ShopManager : IShopManager
    {
        public List<Shop> GetExistingShop()
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                return context.Set<ShopDataModel>()
                    .Select(x => new Shop() { Name = x.Name, Location = x.Location, SubName = x.SubName })
                    .ToList();
            }
        }
    }
}
