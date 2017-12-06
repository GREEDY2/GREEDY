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

            var shops = context.Set<ShopDataModel>()
                    .Select(x => x)
                    .Where(x => x.Name == receipt.Shop.Name); 
            ShopDataModel shopDataModel;

            if (receipt.Shop.Address == null)
            {
                shopDataModel = shops.Where(x => x.Address == null).FirstOrDefault();
            }
            else
            {
                shopDataModel = shops.Select(x => x)
                    .Where(x => x.Address == receipt.Shop.Address).FirstOrDefault();
            }

            ReceiptDataModel receiptDataModel = new ReceiptDataModel()
            {
                ReceiptDate = receipt.ReceiptDate,
                UpdateDate = receipt.UpdateDate,
                Shop = shopDataModel,
                User = userDataModel,
                Total = 0
            };

            receiptDataModel.Items = new List<ItemDataModel>();
            foreach (Item item in receipt.ItemsList)
            {
                receiptDataModel.Items.Add(new ItemDataModel()
                {
                    Receipt = receiptDataModel,
                    Price = item.Price,
                    Name = item.Name,
                    Category = item.Category
                });

                receiptDataModel.Total += item.Price;
            }
            context.Set<ReceiptDataModel>().Add(receiptDataModel);
            context.SaveChanges();
            return receiptDataModel.ReceiptId;
        }

        public List<Item> GetItemsOfSingleReceipt(int receiptId)
        {
            var temp = context.Set<ReceiptDataModel>()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.ReceiptId == receiptId);
            return temp.Items.Select(x => new Item
            {
                Category = x.Category,
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
                Category = x.Category,
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
                    Name = x.Name, Category = x.Category,
                    ItemId = x.ItemId, Price = x.Price }).ToList();
            }
        }

        //TODO: for now this only saves the changed item to ItemDataModels table
        //nothing is written for categorizations.
        //Once categoraziation is sorted out need to add extra logic
        public void UpdateItem(Item updatedItem)
        {
            var itemToUpdate = context.Set<ItemDataModel>()
                .FirstOrDefault(x => x.ItemId == updatedItem.ItemId);
            //TODO: I believe this can be written in more SOLID style
            //Using explicit/implicit type conversion operators
            //Didn't have the time to research this
            itemToUpdate.Name = updatedItem.Name;
            itemToUpdate.Category = updatedItem.Category;
            itemToUpdate.Price = updatedItem.Price;
            context.SaveChanges();
        }

        public void AddItem(Item newItem, int receiptId)
        {
            var receipt = context.Set<ReceiptDataModel>().First(x => x.ReceiptId == receiptId);
            context.Set<ItemDataModel>().Add(new ItemDataModel
            {
                Name = newItem.Name,
                Category = newItem.Category,
                Price = newItem.Price,
                Receipt = receipt
            });
            context.SaveChanges();
        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = context.Set<ItemDataModel>().First(x => x.ItemId == itemId);
            context.Set<ItemDataModel>().Remove(itemToDelete);
            context.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            context.Dispose();
        }
    }
}
