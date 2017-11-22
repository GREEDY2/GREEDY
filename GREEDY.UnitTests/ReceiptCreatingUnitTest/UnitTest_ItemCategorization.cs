using GREEDY.Models;
using GREEDY.ReceiptCreatings;
using System.Collections.Generic;
using Xunit;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class UnitTest_ItemCategorization
    {
        [Fact]
        public void ItemCategorization_CategorizeSingleItem_CategorizingEmptyString()
        {
            //arrange
            var itemCategorization = new ItemCategorization();
            List<ItemInfo> NewData = new List<ItemInfo>();
            //act

            //assert
            Assert.Equal(NewData, itemCategorization.CategorizeAllItems(NewData));
        }
    }
}