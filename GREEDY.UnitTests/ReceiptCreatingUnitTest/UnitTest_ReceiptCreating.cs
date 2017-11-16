using System;
using System.Collections.Generic;
using System.Drawing;
using Ploeh.AutoFixture;
using GREEDY.DataManagers;
using Xunit;
using GREEDY.OCRs;
using Moq;
using GREEDY.ReceiptCreatings;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class UnitTest_ReceiptCreating
    {
        Fixture fixture = new Fixture();
        Mock<IOcr> ocr = new Mock<IOcr>();
        Mock<IShopManager> shops = new Mock<IShopManager>();

         [Fact]
        public void ReceiptCreating_FullReceiptCreating_EmptyBitmap()
        {
            //arrange
            Bitmap image = new Bitmap(1, 1);
            ReceiptCreating receiptCreating = new ReceiptCreating(ocr.Object, shops.Object);
            //act
            //assert
            Assert.Null(receiptCreating.FullReceiptCreating(image));
        }

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_NullBitmap()
        {
            //arrange
            ReceiptCreating receiptCreating = new ReceiptCreating(ocr.Object, shops.Object);
            //act
            //assert
            Assert.Throws<ArgumentNullException>(() => receiptCreating.FullReceiptCreating(null));
        }

        [Fact]
        public void ReceiptCreating_GetDateForReceipt_RandomDate()
        {
            //arrange
            var dateTime = fixture.Create<DateTime>();
            List<string> linesOfText = new List<string>
            {
                dateTime.ToString()
            };
            linesOfText.AddMany(fixture.Create<string>, 10);
            ReceiptCreating receiptCreating = new ReceiptCreating(ocr.Object, shops.Object);
            //act
            //assert
            Assert.Equal(dateTime.ToString("d"), receiptCreating.GetDateForReceipt(linesOfText).ToString());
            
        }

        [Fact]
        public void ReceiptCreating_GetDateForReceipt_TodayDate()
        {
            //arrange
            List<string> linesOfText = new List<string>();
            linesOfText.AddMany(fixture.Create<string>, 10);
            ReceiptCreating receiptCreating = new ReceiptCreating(ocr.Object, shops.Object);
            //act
            //assert
            Assert.Equal(DateTime.Now.ToString("d"), receiptCreating.GetDateForReceipt(linesOfText).ToString());
        }

        [Fact]
        public void ReceiptCreating_GetShopFromData_NotExitingShop()
        {
            //arrange
            List<string> linesOfText = new List<string>();
            linesOfText.AddMany(fixture.Create<string>, 10);
            ReceiptCreating receiptCreating = new ReceiptCreating(ocr.Object, shops.Object);
            //act
            //assert
            Assert.Equal("Neatpažinta", receiptCreating.GetShopFromData(linesOfText).Name);
        }

        //integration test
        [Theory]
        [InlineData("MAXIMA")]
        [InlineData("RIMI")]
        [InlineData("LIDL")]
        [InlineData("PALINK")]
        [InlineData("IKI")]
        public void ReceiptCreating_GetShopFromData_ExitingShop(string  ShopName )
        {
            //arrange
            List<string> linesOfText = new List<string>
            {
                ShopName
            };
            linesOfText.AddMany(fixture.Create<string>, 10);
            ReceiptCreating receiptCreating = new ReceiptCreating(ocr.Object, shops.Object);
            //act
            //assert
            Assert.Equal(ShopName, receiptCreating.GetShopFromData(linesOfText).Name);
        }
    }
}