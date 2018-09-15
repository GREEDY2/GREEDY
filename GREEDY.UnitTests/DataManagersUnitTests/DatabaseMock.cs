using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Geocoding;
using GREEDY.Data;
using Moq;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public static class DatabaseMock
    {
        public static Mock<DataBaseModel> GetDataBaseMock()
        {
            var receipts = new[]
            {
                new ReceiptDataModel {ReceiptId = 1},
                new ReceiptDataModel {ReceiptId = 2},
                new ReceiptDataModel {ReceiptId = 3}
            };

            var shops = new[]
            {
                new ShopDataModel
                {
                    Name = "shop1",
                    Location = new Location(0.0, 0.0),
                    Address = "1600 Pennsylvania Ave SE, Washington, DC 20003, USA",
                    ShopId = 1,
                    Receipts = new[] {receipts[0], receipts[2]}
                },
                new ShopDataModel
                {
                    Name = "shop2",
                    Location = new Location(54.6760394, 25.2738736),
                    Address = "Naugarduko g. 24, Vilnius 03225, Lithuania",
                    ShopId = 2,
                    Receipts = new[] {receipts[1]}
                }
            };

            var categories = new[]
            {
                new CategoryDataModel {CategoryName = "category1"},
                new CategoryDataModel {CategoryName = "category2"},
                new CategoryDataModel {CategoryName = "category3"}
            };
            var items = new[]
            {
                new ItemDataModel
                {
                    Name = "item1",
                    ItemId = 1,
                    Price = 1.00m,
                    Category = categories[0],
                    Receipt = receipts[0]
                },
                new ItemDataModel
                {
                    Name = "item2",
                    ItemId = 2,
                    Price = 1.00m,
                    Category = categories[1],
                    Receipt = receipts[0]
                },
                new ItemDataModel
                {
                    Name = "item3",
                    ItemId = 3,
                    Price = 1.00m,
                    Category = categories[0],
                    Receipt = receipts[1]
                },
                new ItemDataModel
                {
                    Name = "item4",
                    ItemId = 4,
                    Price = 1.00m,
                    Category = categories[2],
                    Receipt = receipts[1]
                },
                new ItemDataModel
                {
                    Name = "item5",
                    ItemId = 5,
                    Price = 1.00m,
                    Category = categories[1],
                    Receipt = receipts[2]
                },
                new ItemDataModel
                {
                    Name = "item6",
                    ItemId = 6,
                    Price = 1.00m,
                    Category = categories[2],
                    Receipt = receipts[2]
                },
                new ItemDataModel
                {
                    Name = "item7",
                    ItemId = 7,
                    Price = 1.00m,
                    Category = categories[2],
                    Receipt = receipts[2]
                }
            };

            var users = new[]
            {
                new UserDataModel
                {
                    Username = "username1",
                    Password = "password1",
                    Email = "email1",
                    FullName = "FullName1",
                    Receipts = new[] {receipts[0], receipts[1]}
                },
                new UserDataModel
                {
                    Username = "username2",
                    Password = "password2",
                    Email = "email2",
                    FullName = "FullName2",
                    Receipts = new[] {receipts[2]}
                }
            };

            receipts[0].Items = new[] {items[0], items[1]};
            receipts[0].Shop = shops[0];
            receipts[0].User = users[0];

            receipts[1].Items = new[] {items[2], items[3]};
            receipts[1].Shop = shops[1];
            receipts[1].User = users[0];

            receipts[2].Items = new[] {items[4], items[5], items[6]};
            receipts[2].Shop = shops[0];
            receipts[2].User = users[1];

            var mockReceipts = MockDbSet(receipts);
            var mockItems = MockDbSet(items);
            var mockUsers = MockDbSet(users);
            var mockShops = MockDbSet(shops);
            var mockCategories = MockDbSet(categories);
            var mockContext = new Mock<DataBaseModel>();
            mockContext.Setup(x => x.Set<ReceiptDataModel>()).Returns(mockReceipts.Object);
            mockContext.Setup(x => x.Set<ShopDataModel>()).Returns(mockShops.Object);
            mockContext.Setup(x => x.Set<ItemDataModel>()).Returns(mockItems.Object);
            mockContext.Setup(x => x.Set<UserDataModel>()).Returns(mockUsers.Object);
            mockContext.Setup(x => x.Set<CategoryDataModel>()).Returns(mockCategories.Object);
            mockContext.Setup(x => x.Receipt).Returns(mockReceipts.Object);
            mockContext.Setup(x => x.Shop).Returns(mockShops.Object);
            mockContext.Setup(x => x.Item).Returns(mockItems.Object);
            mockContext.Setup(x => x.User).Returns(mockUsers.Object);

            return mockContext;
        }

        private static Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            var qdata = data.AsQueryable();
            mockSet.As<IQueryable<T>>().Setup(x => x.Provider).Returns(qdata.Provider);
            mockSet.As<IQueryable<T>>().Setup(x => x.Expression).Returns(qdata.Expression);
            mockSet.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(qdata.ElementType);
            mockSet.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(qdata.GetEnumerator());
            return mockSet;
        }
    }
}