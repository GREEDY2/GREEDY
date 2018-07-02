using System;
using System.Collections.Generic;
using System.Drawing;
using AutoFixture;
using Xunit;
using GREEDY.OCRs;
using Moq;
using System.Threading;
using System.Globalization;
using GREEDY.ReceiptCreating;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class UnitTest_ReceiptCreating
    {
        Fixture fixture = new Fixture();
        Mock<IOcr> ocr = new Mock<IOcr>();
        Mock<IShopDetection> shopDetection = new Mock<IShopDetection>();
        readonly CultureInfo en = new CultureInfo("en-US");
        readonly CultureInfo lt = new CultureInfo("lt-LT");

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_EmptyBitmap()
        {
            //arrange
            Bitmap image = new Bitmap(1, 1);
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
            //act
            //assert
            Assert.Null(receiptCreating.FullReceiptCreating(image));
        }

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_NullBitmap()
        {
            //arrange
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
            //act
            //assert
            Assert.Null(receiptCreating.FullReceiptCreating(null));
        }

        [Fact]
        public void ReceiptCreating_GetDateForReceipt_RandomDate_LTFormat()
        {
            //arrange
            var dateTime = fixture.Create<DateTime>();
            List<string> linesOfText = new List<string>
            {
                dateTime.ToString("d", lt)
            };
            linesOfText.AddMany(fixture.Create<string>, 10);
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
            //act
            //assert
            Assert.Equal(dateTime.ToString("d", en), receiptCreating.GetDateForReceipt(linesOfText).Value.ToString("d"));
        }

        [Fact]
        public void ReceiptCreating_GetDateForReceipt_DateNotFound()
        {
            //arrange
            List<string> linesOfText = new List<string>();
            linesOfText.AddMany(fixture.Create<string>, 10);
            ReceiptMaking receiptCreating = new ReceiptMaking(ocr.Object, shopDetection.Object);
            //act
            //assert
            Assert.Null(receiptCreating.GetDateForReceipt(linesOfText));
        }
    }
}