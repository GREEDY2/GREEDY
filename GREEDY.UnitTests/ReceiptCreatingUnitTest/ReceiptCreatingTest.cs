using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using AutoFixture;
using GREEDY.OCRs;
using GREEDY.ReceiptCreating;
using Moq;
using Xunit;

namespace GREEDY.UnitTests.ReceiptCreatingUnitTest
{
    public class ReceiptCreatingTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IOcr> _ocr = new Mock<IOcr>();
        private readonly Mock<IShopDetection> _shopDetection = new Mock<IShopDetection>();
        private readonly CultureInfo _en = new CultureInfo("en-US");
        private readonly CultureInfo _lt = new CultureInfo("lt-LT");

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_EmptyBitmap()
        {
            //arrange
            var image = new Bitmap(1, 1);
            var receiptCreating = new ReceiptMaking(_ocr.Object, _shopDetection.Object);
            //act
            //assert
            Assert.Null(receiptCreating.FullReceiptCreating(image));
        }

        [Fact]
        public void ReceiptCreating_FullReceiptCreating_NullBitmap()
        {
            //arrange
            var receiptCreating = new ReceiptMaking(_ocr.Object, _shopDetection.Object);
            //act
            //assert
            Assert.Null(receiptCreating.FullReceiptCreating(null));
        }

        [Fact]
        public void ReceiptCreating_GetDateForReceipt_DateNotFound()
        {
            //arrange
            var linesOfText = new List<string>();
            linesOfText.AddMany(_fixture.Create<string>, 10);
            var receiptCreating = new ReceiptMaking(_ocr.Object, _shopDetection.Object);
            //act
            //assert
            Assert.Null(receiptCreating.GetDateForReceipt(linesOfText));
        }

        [Fact]
        public void ReceiptCreating_GetDateForReceipt_RandomDate_LTFormat()
        {
            //arrange
            var dateTime = _fixture.Create<DateTime>();
            var linesOfText = new List<string>
            {
                dateTime.ToString("d", _lt)
            };
            linesOfText.AddMany(_fixture.Create<string>, 10);
            var receiptCreating = new ReceiptMaking(_ocr.Object, _shopDetection.Object);
            //act
            //assert
            Assert.Equal(dateTime.ToString("d", _en),
                receiptCreating.GetDateForReceipt(linesOfText)?.ToString("d"));
        }
    }
}