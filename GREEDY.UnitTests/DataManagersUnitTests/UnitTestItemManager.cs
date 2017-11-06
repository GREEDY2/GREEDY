using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using GREEDY.Data;
using GREEDY.Models;
using System.Data.Entity;
using GREEDY.DataManagers;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTestItemManager
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
            ItemManager itemManager=new ItemManager(DatabaseMock.GetDataBaseMock().Object);
            Assert.Throws<Exception>(() => itemManager.AddItems(new Item[] { },new Shop(),"User that doesn't exist"));
        }

    }
}
