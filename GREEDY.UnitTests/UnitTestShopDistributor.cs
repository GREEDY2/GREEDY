using Xunit;
using Ploeh.AutoFixture;
using System;
using GREEDY.DataManagers;
using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.UnitTests
{
    public class UnitTestShopDistributor
    {
        [Fact]
        public void ShopDistributor_NotExsistingShop_EmtyString()
        {
            //arrange
            var fixture = new Fixture();
            Receipt receipt = fixture.Create<Receipt>();
            ShopDistributor shopDistributor = new ShopDistributor();
            //act

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => shopDistributor.ReceiptDistributor(receipt));
        }

        [Fact]
        public void ShopDistributor_ReceiptWithoutLines_NullException()
        {
            //arrange
            var fixture = new Fixture();
            var receipt = fixture.Build<Receipt>().Without(p => p.LinesOfText).Create();

            ShopDistributor shopDistributor = new ShopDistributor();
            //act

            Assert.Throws(typeof(ArgumentNullException), () => shopDistributor.ReceiptDistributor(receipt));
        }

        [Theory]
        [InlineData("MAXIMA")]
        [InlineData("RIMI")]
        public void ShopDistributor_ExsistingShop_ShopName(string data)
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>();
            list.Add(data);
            fixture.AddManyTo(list);
            Receipt receipt = fixture.Build<Receipt>().With(x => x.LinesOfText, list).Create();

            //act
            ShopDistributor shopDistributor = new ShopDistributor();
            var shopName = shopDistributor.ReceiptDistributor(receipt);

            Assert.Equal(data, shopName);
        }
    }
}
