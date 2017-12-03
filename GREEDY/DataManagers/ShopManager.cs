using GREEDY.Data;
using GREEDY.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace GREEDY.DataManagers
{
    public class ShopManager : IShopManager, IDisposable
    {
        private DbContext context;

        public ShopManager(DbContext context)
        {
            this.context = context;
        }

        public List<Shop> GetExistingShops()
        {
            using (context)
            {
                return context.Set<ShopDataModel>()
                    .Select(x => new Shop() {
                        Name = x.Name,
                        Location = x.Location,
                        Address = x.Address,
                        SubName = x.SubName
                         })
                    .ToList();
            }
        }

        public List<Shop> GetAllUserShops(string username)
        {
            using (context)
            {
                var shops = context.Set<ReceiptDataModel>()
                    .Where(x => x.User.Username == username)
                    .Select(x => x.Shop).ToList();
                return shops.Select(x => new Shop() {
                    Name = x.Name,
                    Address = x.Address,
                    Location = x.Location,
                    SubName = x.SubName }).ToList();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}