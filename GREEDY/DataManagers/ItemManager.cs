using System.Collections.Generic;
using GREEDY.Models;
using GREEDY.Data;
using System.Linq;

namespace GREEDY.DataManagers
{
    public class ItemManager : IItemManager
    {
        public int AddItems(IEnumerable<Item> items, Shop shop, string username)
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                ShopDataModel shopDataModel = context.Set<ShopDataModel>()
                    .Select(x => x)
                    .Where(x => x.Name == shop.Name && x.Location == shop.Location)
                    .FirstOrDefault() ?? new ShopDataModel() { Location = shop.Location, Name = shop.Name };

                UserDataModel userDataModel = context.Set<UserDataModel>()
                    .Select(x => x).Where(x => x.Username == username).FirstOrDefault();
                if (userDataModel == null) throw new System.Exception(Properties.Resources.UserNotFound);

                ReceiptDataModel receiptDataModel = new ReceiptDataModel() { Shop = shopDataModel, User = userDataModel, Total = 0 };
                receiptDataModel.Items = new List<ItemDataModel>();
                foreach (Item item in items)
                {
                    receiptDataModel.Items.Add(new ItemDataModel() { Receipt = receiptDataModel, Price = item.Price, Name = item.Name, Category = item.Category });
                    receiptDataModel.Total += item.Price;
                }
                context.Set<ReceiptDataModel>().Add(receiptDataModel);
                context.SaveChanges();
                return receiptDataModel.ReceiptId;
            }
        }

        public List<Item> GetItemsOfSingleReceipt(int receiptId)
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                var temp = context.Set<ReceiptDataModel>()
                    .FirstOrDefault(x => x.ReceiptId == receiptId);
                return temp.Items.Select(x => new Item { Category = x.Category, Name = x.Name, Price = x.Price, ItemId = x.ItemId }).ToList();
            }
        }

        public List<Item> LoadData(string Username)
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                var temp = context.Set<ItemDataModel>()
                    .Select(x => x)
                    .Where(x => x.Receipt.User.Username == Username);
                return temp.Select(x => new Item { Category = x.Category, Name = x.Name, Price = x.Price }).ToList();
            }
        }

        //TODO: for now this only saves the changed item to ItemDataModels table
        //nothing is written for categorizations.
        //Once categoraziation is sorted out need to add extra logic
        public void UpdateItem(Item UpdatedItem)
        {
            using (DataBaseModel context = new DataBaseModel())
            {
                var itemToUpdate = context.Set<ItemDataModel>()
                    .FirstOrDefault(x => x.ItemId == UpdatedItem.ItemId);
                //TODO: I believe this can be written in more SOLID style
                //Using explicit/implicit type conversion operators
                //Didn't have the time to research this
                itemToUpdate.Name = UpdatedItem.Name;
                itemToUpdate.Category = UpdatedItem.Category;
                itemToUpdate.Price = UpdatedItem.Price;
                context.SaveChanges();
            }
        }
    }
}