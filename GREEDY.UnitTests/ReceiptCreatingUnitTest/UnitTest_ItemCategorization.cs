using GREEDY.Models;
using System.Collections.Generic;
using GREEDY.ReceiptCreating;
using Xunit;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class UnitTest_ItemCategorization
    {
        [Fact]
        public void ItemCategorization_CategorizeItems_CategorizingEmptyString()
        {
            //arrange
            var itemCategorization = new ItemCategorization();
            List<Item> NewData = new List<Item>();
            //act
            //assert
            Assert.Equal(NewData, itemCategorization.CategorizeItems(NewData));
        }
    }
}