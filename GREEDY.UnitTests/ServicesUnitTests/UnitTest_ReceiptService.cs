using System.Drawing;
using GREEDY.DataManagers;
using GREEDY.Services;
using Moq;
using Xunit;
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
            var receiptCreating = new Mock<IReceiptCreating>();
            var dataConverter = new Mock<IDataConverter>();
            ReceiptService receiptService =
                new ReceiptService(
                    imageFormating: imageFormating.Object,
                    receiptCreating: receiptCreating.Object,
                    dataConverter: dataConverter.Object);
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
            var receiptCreating = new Mock<IReceiptCreating>();
            var dataConverter = new Mock<IDataConverter>();
            ReceiptService receiptService =
                new ReceiptService(
                    imageFormating: imageFormating.Object,
                    receiptCreating: receiptCreating.Object,
                    dataConverter: dataConverter.Object);
            //act

            //assert
            Assert.Null(receiptService.ProcessReceiptImage(image));
        }
    }
}
