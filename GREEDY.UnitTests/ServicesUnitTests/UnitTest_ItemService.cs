using System;
using GREEDY.DataManagers;
using GREEDY.ReceiptCreating;
using GREEDY.Services;
using Moq;
using Xunit;

namespace GREEDY.UnitTests.ServicesUnitTests
{
    public class UnitTestItemService
    {
        [Fact]
        public void ItemService_NullStrings_NullException()
        {
            //arrange
            var dataConverter = new Mock<IDataConverter>();
            var dataManager = new Mock<IItemManager>();
            var itemCategorization = new Mock<IItemCategorization>();
            ItemService itemService = new ItemService(itemCategorization.Object);
            //act
            //assert
            Assert.Throws<NullReferenceException>(() => itemService.AddCategory(null, null));
        }
    }
}