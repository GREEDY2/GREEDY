using System.Collections.Generic;
using AutoFixture;
using GREEDY.Models;
using GREEDY.ReceiptCreating;
using Xunit;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class UnitTestDataConverter
    {
        [Fact]
        public void DataConverter_ReceiptToItemList_IKI_ItemToMatchRegex()
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
            var receipt = new Receipt
            {
                LinesOfText = list
            };
            var dataConverter = new DataConverter();
            //act
            //assert
            Assert.True(dataConverter.ReceiptToItemList(receipt).Count == 2);
        }

        [Fact]
        public void DataConverter_ReceiptToItemList_MAXIMA_ItemToMatchRegex()
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>
            {
                "MAXIMA LI, UAB\r\r\nPVM mokėtojo kodas LT230335113\r\r\nKvitas 184/54 A00010400 .\r\r\n",
                "Bananai \r\r\n\r\r\n0,69 X 1,494 kg 1,03 A\r\r\nPVM Be PVM Su PVM |\r\r\n"
            };
            fixture.AddManyTo(list);
            var receipt = new Receipt
            {
                LinesOfText = list
            };
            var dataConverter = new DataConverter();
            //act
            //assert
            Assert.True(dataConverter.ReceiptToItemList(receipt).Count == 1);
        }

        [Fact]
        public void DataConverter_ReceiptToItemList_NoItemsToMatchRegex()
        {
            //arrange
            var fixture = new Fixture();
            var list = new List<string>();
            fixture.AddManyTo(list);
            var receipt = new Receipt
            {
                LinesOfText = list
            };
            var dataConverter = new DataConverter();
            //act
            //assert
            Assert.True(dataConverter.ReceiptToItemList(receipt).Count == 0);
        }
    }
}