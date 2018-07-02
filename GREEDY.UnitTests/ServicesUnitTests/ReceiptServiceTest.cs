using GREEDY.ImagePreparation;
using GREEDY.ReceiptCreating;
using GREEDY.Services;
using Moq;
using OpenCvSharp;
using Xunit;

namespace GREEDY.UnitTests.ServicesUnitTests
{
    public class ReceiptServiceTest
    {
        [Fact]
        public void ReceiptService_EmptyImage_ReturnNull()
        {
            //arrange
            var imageFormating = new Mock<IImageFormating>();
            var image = new Mat();
            var receiptCreating = new Mock<IReceiptMaking>();
            var dataConverter = new Mock<IDataConverter>();
            var itemCategorization = new Mock<IItemCategorization>();
            var receiptService =
                new ReceiptService(
                    imageFormating.Object,
                    receiptCreating.Object,
                    dataConverter.Object,
                    itemCategorization.Object);
            //act
            //assert
            Assert.Null(receiptService.ProcessReceiptImage(image));
        }

        [Fact]
        public void ReceiptService_NoImage_NullBitmap()
        {
            //arrange
            var imageFormating = new Mock<IImageFormating>();
            var receiptCreating = new Mock<IReceiptMaking>();
            var dataConverter = new Mock<IDataConverter>();
            var itemCategorization = new Mock<IItemCategorization>();
            var receiptService =
                new ReceiptService(
                    imageFormating.Object,
                    receiptCreating.Object,
                    dataConverter.Object,
                    itemCategorization.Object);
            //act
            //assert
            Assert.Null(receiptService.ProcessReceiptImage(null));
        }
    }
}