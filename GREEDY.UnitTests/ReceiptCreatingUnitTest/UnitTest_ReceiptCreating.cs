using System;
using System.Collections.Generic;
using System.Drawing;
using AutoFixture;
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
        Mock<IShopDetection> shopDetection = new Mock<IShopDetection>();

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_EmptyBitmap()
        {
            //arrange
            Bitmap image = new Bitmap(1, 1);
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
            //act
            //assert
            Assert.Throws<ArgumentNullException>(() => receiptCreating.FullReceiptCreating(image));
        }

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_NullBitmap()
        {
            //arrange
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
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
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
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
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
            //act
            //assert
            Assert.Equal(DateTime.Now.ToString("d"), receiptCreating.GetDateForReceipt(linesOfText).ToString());
        }
    }
}