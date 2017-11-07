using System;
using System.Linq;
using GREEDY.Models;
using GREEDY.DataManagers;
using Xunit;
using Ploeh.AutoFixture;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTest_ItemManager
    {
        [Fact]
        public void ItemManager_LoadData()
        {
            var items = new ItemManager(DatabaseMock.GetDataBaseMock().Object).LoadData("username1");
            Assert.Equal(4, items.Count());
        }

        [Fact]
        public void ItemManager_LoadData_UsernameCaseDoesNotMatch()
        {
            var items = new ItemManager(DatabaseMock.GetDataBaseMock().Object).LoadData("UsErNaMe1");
            Assert.Equal(4, items.Count());
        }

        [Fact]
        public void ItemManager_GetItemsOfSingleReceipt()
        {
            var items = new ItemManager(DatabaseMock.GetDataBaseMock().Object).GetItemsOfSingleReceipt(1);
            Assert.Equal(2, items.Count());
        }

        [Fact]
        public void ItemManager_LoadData_UserNotFound()
        {
            var shop = new Fixture().Create<Shop>();
            ItemManager itemManager=new ItemManager(DatabaseMock.GetDataBaseMock().Object);
            Assert.Throws<Exception>(() => itemManager.AddItems(new Item[] { }, shop, "User that doesn't exist"));
        }
    }
}
