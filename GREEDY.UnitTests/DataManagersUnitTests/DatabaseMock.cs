using System.Linq;
using GREEDY.Data;
using Moq;
using System.Data.Entity;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public static class DatabaseMock
    {
        public static Mock<DataBaseModel> GetDataBaseMock()
        {
            var Receipts = new ReceiptDataModel[]
            {
                new ReceiptDataModel{ReceiptId=1},
                new ReceiptDataModel{ReceiptId=2},
                new ReceiptDataModel{ReceiptId=3}
            };

            var Shops = new ShopDataModel[]
            {
                new ShopDataModel{Name="shop1",Location="location1",ShopId=1,Receipts=new ReceiptDataModel[]{Receipts[0],Receipts[2] } },
                new ShopDataModel{Name="shop2",Location="location2",ShopId=2,Receipts=new ReceiptDataModel[]{Receipts[1] } }
            };

            var Items = new ItemDataModel[]
                {
                    new ItemDataModel{Name="item1",ItemId=1,Price=1.00m,Category="category",Receipt=Receipts[0]},
                    new ItemDataModel{Name="item2",ItemId=2,Price=1.00m,Category="category",Receipt=Receipts[0]},
                    new ItemDataModel{Name="item3",ItemId=3,Price=1.00m,Category="category",Receipt=Receipts[1]},
                    new ItemDataModel{Name="item4",ItemId=4,Price=1.00m,Category="category",Receipt=Receipts[1]},
                    new ItemDataModel{Name="item5",ItemId=5,Price=1.00m,Category="category",Receipt=Receipts[2]},
                    new ItemDataModel{Name="item6",ItemId=6,Price=1.00m,Category="category",Receipt=Receipts[2]},
                    new ItemDataModel{Name="item7",ItemId=7,Price=1.00m,Category="category",Receipt=Receipts[2]},
                };

            var Users = new UserDataModel[]
                {
                    new UserDataModel(){Username="username1",Password="password1",Email="email1",FullName="FullName1",Receipts=new ReceiptDataModel[]{Receipts[0],Receipts[1]} },
                    new UserDataModel(){Username="username2",Password="password2",Email="email2",FullName="FullName2",Receipts=new ReceiptDataModel[]{Receipts[2]} },
                };

            Receipts[0].Items = new ItemDataModel[] { Items[0], Items[1] };
            Receipts[0].Shop = Shops[0];
            Receipts[0].User = Users[0];


            Receipts[1].Items = new ItemDataModel[] { Items[2], Items[3] };
            Receipts[1].Shop = Shops[1];
            Receipts[1].User = Users[0];


            Receipts[2].Items = new ItemDataModel[] { Items[4], Items[5], Items[6] };
            Receipts[2].Shop = Shops[0];
            Receipts[2].User = Users[1];

            var mockReceipts = MockDbSet<ReceiptDataModel>(Receipts);
            var mockItems = MockDbSet<ItemDataModel>(Items);
            var mockUsers = MockDbSet<UserDataModel>(Users);
            var mockShops = MockDbSet<ShopDataModel>(Shops);
            var mockContext = new Mock<DataBaseModel>();
            mockContext.Setup(x => x.Set<ReceiptDataModel>()).Returns(mockReceipts.Object);
            mockContext.Setup(x => x.Set<ShopDataModel>()).Returns(mockShops.Object);
            mockContext.Setup(x => x.Set<ItemDataModel>()).Returns(mockItems.Object);
            mockContext.Setup(x => x.Set<UserDataModel>()).Returns(mockUsers.Object);
            mockContext.Setup(x => x.Receipt).Returns(mockReceipts.Object);
            mockContext.Setup(x => x.Shop).Returns(mockShops.Object);
            mockContext.Setup(x => x.Item).Returns(mockItems.Object);
            mockContext.Setup(x => x.User).Returns(mockUsers.Object);

            return mockContext;
        }

        private static Mock<DbSet<T>> MockDbSet<T>(T[] data) where T : class
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
