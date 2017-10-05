using System;
using System.Drawing;
using GREEDY.DataManagers;
using GREEDY.OCRs;
using GREEDY.Services;
using Moq;
using Xunit;

namespace GREEDY.UnitTests
{
    public class UnitTestItemService
    {
        [Fact]
        public void ItemService_NullStrings_NullException()
        {
            //arrange
            var dataConverter = new Mock<IDataConverter>();
            var dataManager = new Mock<IDataManager>();
            var itemCategorization = new Mock<IItemCategorization>();
            ItemService itemService = new ItemService(dataConverter.Object, dataManager.Object, itemCategorization.Object);
            //act

            //assert
            Assert.Throws(typeof(NullReferenceException), () => itemService.AddChangeCategory(null, null));
        }
    }
}