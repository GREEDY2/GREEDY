using System.Drawing;
using GREEDY.Services;
using Moq;
using Xunit;
using GREEDY.ImagePreparation;
using GREEDY.ReceiptCreatings;

namespace GREEDY.UnitTests.ServicesUnitTests
{
    public class UnitTest_ReceiptService
    {
        [Fact]
        public void ReceiptService_NoImage_NullBitmap()
        {
            //arrange
            var imageFormating = new Mock<IImageFormating>();
            var receiptCreating = new Mock<IReceiptMaking>();
            var dataConverter = new Mock<IDataConverter>();
            var itemCategorization = new Mock<IItemCategorization>();
            ReceiptService receiptService =
                new ReceiptService(
                    imageFormating: imageFormating.Object,
                    receiptCreating: receiptCreating.Object,
                    dataConverter: dataConverter.Object,
                    itemCategorization: itemCategorization.Object);
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
            var receiptCreating = new Mock<IReceiptMaking>();
            var dataConverter = new Mock<IDataConverter>();
            var itemCategorization = new Mock<IItemCategorization>();
            ReceiptService receiptService =
                new ReceiptService(
                    imageFormating: imageFormating.Object,
                    receiptCreating: receiptCreating.Object,
                    dataConverter: dataConverter.Object,
                    itemCategorization : itemCategorization.Object);
            //act

            //assert
            Assert.Null(receiptService.ProcessReceiptImage(image));
        }
    }
}
