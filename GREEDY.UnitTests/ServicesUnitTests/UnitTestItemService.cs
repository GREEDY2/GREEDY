using System;
using GREEDY.DataManagers;
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
            var itemCategorization = new Mock<IItemCategorization>();
            ItemService itemService = new ItemService(itemCategorization.Object);
            //act

            //assert
            Assert.Throws(typeof(NullReferenceException), () => itemService.AddChangeCategory(null, null));
        }
    }
}