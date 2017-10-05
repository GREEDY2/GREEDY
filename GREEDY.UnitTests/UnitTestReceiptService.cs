using Xunit;
using GREEDY.DataManagers;
using GREEDY.Services;
using GREEDY.OCRs;
using System.Drawing;
using Moq;

namespace GREEDY.UnitTests
{
    public class UnitTestReceiptService
    {
        [Fact]
        public void ReceiptService_NoImage_NullBitmap()
        {
            //arrange
            var ocr = new Mock<IOcr>();
            var dataConverter = new Mock<IDataConverter>();
            var dataManager = new Mock<IDataManager>();
            ReceiptService receiptService = new ReceiptService(ocr.Object, dataConverter.Object, dataManager.Object);
            //act

            //assert
            Assert.Null(receiptService.ProcessReceiptImage(null));
        }

        [Fact]
        public void ReceiptService_EmptyImage_ReturnNull()
        {
            //arrange
            Bitmap image = new Bitmap(1, 1);
            var ocr = new Mock<IOcr>();
            var dataConverter = new Mock<IDataConverter>();
            var dataManager = new Mock<IDataManager>();
            ReceiptService receiptService = new ReceiptService(ocr.Object, dataConverter.Object, dataManager.Object);
            //act

            //assert
            Assert.Null(receiptService.ProcessReceiptImage(image));
        }
    }
}
