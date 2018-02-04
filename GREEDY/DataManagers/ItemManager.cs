using System.Collections.Generic;
using GREEDY.Models;
using GREEDY.Data;
using System.Linq;
using System.Data.Entity;
using System;

namespace GREEDY.DataManagers
{
    public class ItemManager : IItemManager, IDisposable
    {
        private DbContext context;
        public ItemManager(DbContext context)
        {
            this.context = context;
        }
        public int AddItems(Receipt receipt, string username)
        {
            UserDataModel userDataModel = context.Set<UserDataModel>()
                .FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
            if (userDataModel == null)
            {
                throw new Exception(Properties.Resources.UserNotFound);
            }

            ShopDataModel shopDataModel;
            if (receipt.Shop == null)
            {
                shopDataModel = null;
            }
            else
            {
                var shops = context.Set<ShopDataModel>()
                        .Select(x => x)
                        .Where(x => x.Name == receipt.Shop.Name);

                if (receipt.Shop.Address == null)
                {
                    shopDataModel = shops.Where(x => x.Address == null).FirstOrDefault();
                }
                else
                {
                    shopDataModel = shops.Select(x => x)
                        .Where(x => x.Address == receipt.Shop.Address).FirstOrDefault();
                }
            }

            ReceiptDataModel receiptDataModel = new ReceiptDataModel()
            {
                ReceiptDate = receipt.ReceiptDate,
                UpdateDate = receipt.UpdateDate,
                Shop = shopDataModel,
                User = userDataModel,
                Total = 0
            };

            var categories = context.Set<CategoryDataModel>().Select(x => x).ToList();

            receiptDataModel.Items = new List<ItemDataModel>();
            foreach (Item item in receipt.ItemsList)
            {
                receiptDataModel.Items.Add(new ItemDataModel()
                {
                    Receipt = receiptDataModel,
                    Price = item.Price,
                    Name = item.Name,
                    Category = categories.Where(x => x.CategoryName == item.Category)
                        .FirstOrDefault()
            });

                receiptDataModel.Total += item.Price;
            }
            context.Set<ReceiptDataModel>().Add(receiptDataModel);
            context.SaveChanges();
            return receiptDataModel.ReceiptId;
        }

        public List<Item> GetItemsOfSingleReceipt(int receiptId)
        {
            //get exception during unitTesting. Need time ti verify issue
            var temp = context.Set<ReceiptDataModel>()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.ReceiptId == receiptId);

            var categories = context.Set<CategoryDataModel>()
                .Select(x => x);

            return temp.Items.Select(x => new Item
            {
                Category = x.Category.CategoryName,
                Name = x.Name,
                Price = x.Price,
                ItemId = x.ItemId
            }).ToList();
        }

        public List<Item> GetItemsOfSingleCategory(string category)
        {
            var temp = context.Set<CategoryDataModel>()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.CategoryName == category);

            return temp.Items.Select(x => new Item
            {
                Name = x.Name,
                Price = x.Price,
                ItemId = x.ItemId
            }).ToList();
        }

        public List<Item> LoadData(string Username)
        {
            var temp = context.Set<ItemDataModel>()
                     .Select(x => x)
                     .Where(x => x.Receipt.User.Username.ToLower() == Username.ToLower());
            return temp.Select(x => new Item
            {
                Category = x.Category.CategoryName,
                Name = x.Name,
                Price = x.Price
            }).ToList();
        }

        public List<Item> GetAllUserItems(string username)
        {
            using (context)
            {
                var items = context.Set<ItemDataModel>()
                    .Select(x => x).Where(x => x.Receipt.User.Username == username);
                return items.Select(x => new Item() {
                    Name = x.Name, Category = x.Category.CategoryName,
                    ItemId = x.ItemId, Price = x.Price }).ToList();
            }
        }

        public void UpdateItem(Item updatedItem)
        {
            var categories = context.Set<CategoryDataModel>().Select(x => x);
            var itemToUpdate = context.Set<ItemDataModel>()
                .FirstOrDefault(x => x.ItemId == updatedItem.ItemId);
            //TODO: I believe this can be written in more SOLID style
            //Using explicit/implicit type conversion operators
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Category = categories.Where(x => x.CategoryName == updatedItem.Category).FirstOrDefault();
            itemToUpdate.Receipt.Total += updatedItem.Price - itemToUpdate.Price;
            itemToUpdate.Price = updatedItem.Price;
            context.SaveChanges();
        }

        public void AddItem(Item newItem, int receiptId)
        {
            var receipt = context.Set<ReceiptDataModel>().First(x => x.ReceiptId == receiptId);
            var categories = context.Set<CategoryDataModel>().Select(x => x);
            receipt.Total += newItem.Price;
            context.Set<ItemDataModel>().Add(new ItemDataModel
            {
                Name = newItem.Name,
                Category = categories.Where(x => x.CategoryName == newItem.Category).FirstOrDefault(),
                Price = newItem.Price,
                Receipt = receipt
            });
            context.SaveChanges();
        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = context.Set<ItemDataModel>().First(x => x.ItemId == itemId);
            itemToDelete.Receipt.Total -= itemToDelete.Price;
            context.Set<ItemDataModel>().Remove(itemToDelete);
            context.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
