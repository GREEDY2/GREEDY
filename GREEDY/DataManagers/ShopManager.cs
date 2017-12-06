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
                    .Where(x => x.Address != null)
                    .Select(x => new Shop()
                    {
                        Name = x.Name,
                        Location = x.Location,
                        Address = x.Address,
                        SubName = x.SubName
                    })
                    .ToList();
            }
        }

        public List<ShopData> GetAllUserShops(string username)
        {
            using (context)
            {
                var AllUserReceipts = context.Set<ReceiptDataModel>()
                    .Where(x => x.User.Username == username
                    && x.Shop.Address != null).GroupBy(x => x.Shop).ToList();

                return AllUserReceipts.Select(x => new ShopData
                {
                    Name = x.Key.Name,
                    Location = x.Key.Location,
                    Address = x.Key.Address,
                    Total = x.Select(y => y.Total).Sum(),
                    ReceiptNumber = x.Key.Receipts.Count(),
                    Date = x.Select(y => y.ReceiptDate).Last().Value.ToString("d")
                        ?? x.Select(y => y.UpdateDate).Last().ToString("d")
                }).ToList();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}