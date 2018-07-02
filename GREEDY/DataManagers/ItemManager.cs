using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GREEDY.Data;
using GREEDY.Models;
using GREEDY.Properties;

namespace GREEDY.DataManagers
{
    public class ItemManager : IItemManager, IDisposable
    {
        private readonly DbContext _context;

        public ItemManager(DbContext context)
        {
            _context = context;
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }

        public int AddItems(Receipt receipt, string username)
        {
            var userDataModel = _context.Set<UserDataModel>()
                .FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));
            if (userDataModel == null) throw new Exception(Resources.UserNotFound);

            ShopDataModel shopDataModel;
            if (receipt.Shop == null)
            {
                shopDataModel = null;
            }
            else
            {
                var shops = _context.Set<ShopDataModel>()
                    .Select(x => x)
                    .Where(x => x.Name == receipt.Shop.Name);

                if (receipt.Shop.Address == null)
                    shopDataModel = shops.FirstOrDefault(x => x.Address == null);
                else
                    shopDataModel = shops
                        .Select(x => x).FirstOrDefault(x => x.Address == receipt.Shop.Address);
            }

            var receiptDataModel = new ReceiptDataModel
            {
                ReceiptDate = receipt.ReceiptDate,
                UpdateDate = receipt.UpdateDate,
                Shop = shopDataModel,
                User = userDataModel,
                Total = 0
            };

            var categories = _context.Set<CategoryDataModel>().Select(x => x).ToList();

            receiptDataModel.Items = new List<ItemDataModel>();
            foreach (var item in receipt.ItemsList)
            {
                receiptDataModel.Items.Add(new ItemDataModel
                {
                    Receipt = receiptDataModel,
                    Price = item.Price,
                    Name = item.Name,
                    Category = categories
                        .FirstOrDefault(x => x.CategoryName == item.Category)
                });

                receiptDataModel.Total += item.Price;
            }

            _context.Set<ReceiptDataModel>().Add(receiptDataModel);
            _context.SaveChanges();
            return receiptDataModel.ReceiptId;
        }

        public List<Item> GetItemsOfSingleReceipt(int receiptId)
        {
            //get exception during unitTesting. Need time ti verify issue
            var temp = _context.Set<ReceiptDataModel>()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.ReceiptId == receiptId);

            var categories = _context.Set<CategoryDataModel>()
                .Select(x => x);

            return temp?.Items.Select(x => new Item
            {
                Category = x.Category.CategoryName,
                Name = x.Name,
                Price = x.Price,
                ItemId = x.ItemId
            }).ToList();
        }

        public List<Item> GetAllUserItems(string username)
        {
            using (_context)
            {
                var items = _context.Set<ItemDataModel>()
                    .Select(x => x).Where(x => x.Receipt.User.Username == username);
                return items.Select(x => new Item
                {
                    Name = x.Name,
                    Category = x.Category.CategoryName,
                    ItemId = x.ItemId,
                    Price = x.Price
                }).ToList();
            }
        }

        public void UpdateItem(Item updatedItem)
        {
            var categories = _context.Set<CategoryDataModel>().Select(x => x);
            var itemToUpdate = _context.Set<ItemDataModel>()
                .FirstOrDefault(x => x.ItemId == updatedItem.ItemId);
            //TODO: I believe this can be written in more SOLID style
            //Using explicit/implicit type conversion operators
            if (itemToUpdate != null)
            {
                itemToUpdate.Name = updatedItem.Name;
                itemToUpdate.Category = categories.FirstOrDefault(x => x.CategoryName == updatedItem.Category);
                itemToUpdate.Receipt.Total += updatedItem.Price - itemToUpdate.Price;
                itemToUpdate.Price = updatedItem.Price;
            }

            _context.SaveChanges();
        }

        public void AddItem(Item newItem, int receiptId)
        {
            var receipt = _context.Set<ReceiptDataModel>().First(x => x.ReceiptId == receiptId);
            var categories = _context.Set<CategoryDataModel>().Select(x => x);
            receipt.Total += newItem.Price;
            _context.Set<ItemDataModel>().Add(new ItemDataModel
            {
                Name = newItem.Name,
                Category = categories.FirstOrDefault(x => x.CategoryName == newItem.Category),
                Price = newItem.Price,
                Receipt = receipt
            });
            _context.SaveChanges();
        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = _context.Set<ItemDataModel>().First(x => x.ItemId == itemId);
            itemToDelete.Receipt.Total -= itemToDelete.Price;
            _context.Set<ItemDataModel>().Remove(itemToDelete);
            _context.SaveChanges();
        }

        public List<Item> GetItemsOfSingleCategory(string category)
        {
            var temp = _context.Set<CategoryDataModel>()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.CategoryName == category);

            return temp?.Items.Select(x => new Item
            {
                Name = x.Name,
                Price = x.Price,
                ItemId = x.ItemId
            }).ToList();
        }

        public List<Item> LoadData(string username)
        {
            var temp = _context.Set<ItemDataModel>()
                .Select(x => x)
                .Where(x => x.Receipt.User.Username.ToLower() == username.ToLower());
            return temp.Select(x => new Item
            {
                Category = x.Category.CategoryName,
                Name = x.Name,
                Price = x.Price
            }).ToList();
        }
    }
}