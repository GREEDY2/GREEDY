using System.Collections.Generic;
using AutoFixture;
using Geocoding;
using GREEDY.ReceiptCreating;
using Xunit;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class ShopDetectionTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Location _location = new Location(54.7076415, 25.2200897);
        private readonly ShopDetection _shopDetection = new ShopDetection();

        //integration test
        [Theory]
        [InlineData("MAXIMA")]
        [InlineData("RIMI")]
        [InlineData("LIDL")]
        [InlineData("PALINK")]
        [InlineData("IKI")]
        public void ShopDetection_GetShopFromData_ExitingShop(string shopName)
        {
            //arrange
            var linesOfText = new List<string>
            {
                shopName + " "
            };
            linesOfText.AddMany(_fixture.Create<string>, 10);
            //act
            //assert
            Assert.Equal(shopName, _shopDetection.GetShopFromData(linesOfText).Name);
        }

        [Fact]
        public void ShopDetection_GetShopFromData_NotExistingShop()
        {
            //arrange
            var linesOfText = new List<string>();
            linesOfText.AddMany(_fixture.Create<string>, 10);
            //act
            //assert
            Assert.Equal("Neatpažinta", _shopDetection.GetShopFromData(linesOfText).Name);
        }
    }
}