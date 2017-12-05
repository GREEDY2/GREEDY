using Ploeh.AutoFixture;
using System.Collections.Generic;
using Xunit;
using Geocoding;
using GREEDY.ReceiptCreatings;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTest_ShopDetection
    {
        Fixture fixture = new Fixture();
        Location location = new Location(54.7076415, 25.2200897);
        ShopDetection shopDetection = new ShopDetection();

        [Fact]
        public void ShopDetection_GetShopFromData_NotExistingShop()
        {
            //arrange
            List<string> linesOfText = new List<string>();
            linesOfText.AddMany(fixture.Create<string>, 10);
            //act
            //assert
            Assert.Equal("Neatpažinta", shopDetection.GetShopFromData(linesOfText).Name);
        }

        //integration test
        [Theory]
        [InlineData("MAXIMA")]
        [InlineData("RIMI")]
        [InlineData("LIDL")]
        [InlineData("PALINK")]
        [InlineData("IKI")]
        public void ShopDetection_GetShopFromData_ExitingShop(string ShopName)
        {
            //arrange
            List<string> linesOfText = new List<string>
            {
                ShopName
            };
            linesOfText.AddMany(fixture.Create<string>, 10);
            //act
            //assert
            Assert.Equal(ShopName, shopDetection.GetShopFromData(linesOfText).Name);
        }
    }
}