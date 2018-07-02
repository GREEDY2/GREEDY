using System.Collections.Generic;
using GREEDY.Models;
using GREEDY.ReceiptCreating;
using Xunit;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class ItemCategorizationTest
    {
        [Fact]
        public void ItemCategorization_CategorizeItems_CategorizingEmptyString()
        {
            //arrange
            var itemCategorization = new ItemCategorization();
            var newData = new List<Item>();
            //act
            //assert
            Assert.Equal(newData, itemCategorization.CategorizeItems(newData));
        }
    }
}