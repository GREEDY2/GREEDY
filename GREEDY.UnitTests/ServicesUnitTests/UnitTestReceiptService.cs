using System.Drawing;
using GREEDY.DataManagers;
using GREEDY.OCRs;
using GREEDY.Services;
using Moq;
using Xunit;

namespace GREEDY.UnitTests.ServicesUnitTests
{
    public class UnitTestReceiptService
    {
        [Fact]
        public void ReceiptService_NoImage_NullBitmap()
        {
            //arrange
            var imageFormating = new Mock<IImageFormating>();
            var ocr = new Mock<IOcr>();
            var dataConverter = new Mock<IDataConverter>();
            var dataManager = new Mock<IItemManager>();
            ReceiptService receiptService = 
                new ReceiptService(
                    imageFormating: imageFormating.Object, 
                    ocr: ocr.Object, 
                    dataConverter: dataConverter.Object, 
                    dataManager: dataManager.Object);
            //act

            //assert
            Assert.Null(receiptService.ProcessReceiptImage(null));
        }

        [Fact]
        public void ReceiptService_EmptyImage_ReturnNull()
        {
            //arrange
            var imageFormating = new Mock<IImageFormating>();
            Bitmap image = new Bitmap(1, 1);
            var ocr = new Mock<IOcr>();
            var dataConverter = new Mock<IDataConverter>();
            var dataManager = new Mock<IItemManager>();
            ReceiptService receiptService =
                new ReceiptService(
                    imageFormating: imageFormating.Object,
                    ocr: ocr.Object,
                    dataConverter: dataConverter.Object,
                    dataManager: dataManager.Object);
            //act

            //assert
            Assert.Null(receiptService.ProcessReceiptImage(image));
        }
    }
}
