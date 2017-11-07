using GREEDY.DataManagers;
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
           
            //act

            //assert
            Assert.Equal(string.Empty, itemCategorization.CategorizeSingleItem(string.Empty));
        }
    }
}