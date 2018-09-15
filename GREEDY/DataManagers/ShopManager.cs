using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public class ShopManager : IShopManager, IDisposable
    {
        private readonly DbContext _context;

        public ShopManager(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Shop> GetExistingShops()
        {
            using (_context)
            {
                return _context.Set<ShopDataModel>()
                    .Where(x => x.Address != null)
                    .Select(x => new Shop
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
            using (_context)
            {
                var allUserReceipts = _context.Set<ReceiptDataModel>()
                    .Where(x => x.User.Username == username
                                && x.Shop.Address != null).GroupBy(x => x.Shop).ToList();

                return allUserReceipts.Select(x => new ShopData
                {
                    Name = x.Key.Name,
                    Location = x.Key.Location,
                    Address = x.Key.Address,
                    Total = x.Select(y => y.Total).Sum(),
                    ReceiptNumber = x.Key.Receipts.Count(),
                    Date = x.Select(y => y.ReceiptDate).Last().HasValue
                        ? x.Select(y => y.ReceiptDate).Last()?.ToString("d")
                        : x.Select(y => y.UpdateDate).Last().ToString("d")
                }).ToList();
            }
        }
    }
}