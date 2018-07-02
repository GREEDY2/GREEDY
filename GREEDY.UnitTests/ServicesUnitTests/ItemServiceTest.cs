using System;
using GREEDY.DataManagers;
using GREEDY.ReceiptCreating;
using GREEDY.Services;
using Moq;
using Xunit;

namespace GREEDY.UnitTests.ServicesUnitTests
{
    public class ItemServiceTest
    {
        [Fact]
        public void ItemService_NullStrings_NullException()
        {
            //arrange
            var itemCategorization = new Mock<IItemCategorization>();
            var itemService = new ItemService(itemCategorization.Object);
            //act
            //assert
            Assert.Throws<NullReferenceException>(() => itemService.AddCategory(null, null));
        }
    }
}