using System;
using System.Collections.Generic;
using System.Linq;
using GREEDY.DataManagers;
using GREEDY.Models;
using Ploeh.AutoFixture;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTestDataConverter
    {
        [Theory]
        [InlineData("MAXIMA")]
        [InlineData("IKI")]
        public void DataConverter_ReceiptToItemList_NoItemsToMatchRegex(string data)
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>
            {
                data + "PALINK"
            };
            fixture.AddManyTo(list);
            Receipt receipt = fixture.Build<Receipt>().With(x => x.LinesOfText, list).Create();
            var dataConverter = new DataConverter();
            //act

            //assert
            Assert.True(dataConverter.ReceiptToItemList(receipt).Count == 0);
        }

        [Theory]
        [InlineData("MAXIMA")]
        [InlineData("IKI")]
        public void DataConverter_ReceiptToItemList_MAXIMA_ItemToMatchRegex(string data)
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>
            {
                data,
                " #12345678 \n",
                "LT123456789\n",
                "vanduo 5,22 A \n",
                "Prekiautojo 5,12 A",
                "PVM  "
            };

            fixture.AddManyTo(list);
            Receipt receipt = fixture.Build<Receipt>().With(x => x.LinesOfText, list).Create();
            var dataConverter = new DataConverter();
            //act

            //assert
            Assert.True(dataConverter.ReceiptToItemList(receipt).Count == 1);
            //Assert.Equal("vanduo", dataConverter.ReceiptToItemList(receipt)[0].Name);
            //Assert.Equal((decimal)5.22, dataConverter.ReceiptToItemList(receipt)[0].Price);
        }
    }
}
