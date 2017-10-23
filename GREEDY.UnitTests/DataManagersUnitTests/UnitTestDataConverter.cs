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
        public void DataConverter_ReceiptToItemList_MAXIMA_ItemToMatchRegex(string data)
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>
            {
                "MAXIMA LI, UAB\r\r\nPVM mokėtojo kodas LT230335113\r\r\nKvitas 184/54 A00010400 .\r\r\n",
                "Bananai \r\r\n\r\r\n0,69 X 1,494 kg 1,03 A\r\r\nPVM Be PVM Su PVM |\r\r\n"
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

        [Theory]
        [InlineData("IKI")]
        public void DataConverter_ReceiptToItemList_IKI_ItemToMatchRegex(string data)
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>
            {
                "PALINK\r\r\n" +
                "LT101937219\r\r\n",
                "VILKYŠKIU 1,14 A\r\r\n",
                "BANANAI 0›60 A\r\r\n",
                "Prekiautojo 10 15057003 \r\r\n"
            };

            fixture.AddManyTo(list);
            Receipt receipt = fixture.Build<Receipt>().With(x => x.LinesOfText, list).Create();
            var dataConverter = new DataConverter();
            //act

            //assert
            Assert.True(dataConverter.ReceiptToItemList(receipt).Count == 2);
            //Assert.Equal("VILKYŠKIU", dataConverter.ReceiptToItemList(receipt)[1].Name);
            //Assert.Equal("BANANAI", dataConverter.ReceiptToItemList(receipt)[1].Name);
            //Assert.Equal((decimal)1.14, dataConverter.ReceiptToItemList(receipt)[1].Price);
            //Assert.Equal((decimal)0.60, dataConverter.ReceiptToItemList(receipt)[1].Price);
        }
    }
}
