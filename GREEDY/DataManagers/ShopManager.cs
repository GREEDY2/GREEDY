using GREEDY.Data;
using GREEDY.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GREEDY.DataManagers
{
    public class ShopManager : IShopManager
    {
        private DbContext context;

        public ShopManager(DbContext context)
        {
            this.context = context;
        }

        public List<Shop> GetExistingShop()
        {
            using (context)
            {
                return context.Set<ShopDataModel>()
                    .Select(x => new Shop() { Name = x.Name, Location = x.Location, SubName = x.SubName })
                    .ToList();
            }
        }
    }
}