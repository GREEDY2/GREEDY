

using GREEDY.DataManagers;
using System.Drawing;
using Xunit;

namespace GREEDY.UnitTests.ImagePreparationUnitTest
{
    public class UnitTest_ImageFormating
    {
        [Fact]
        public void ImageFormating_BradleyLocalThreshold_FilteringGivenBitmap()
        {
            //arrange
            Bitmap image = new Bitmap(1, 1);
            ImageFormating imageFormating = new ImageFormating();
            //act

            //assert
            Assert.Equal(image, imageFormating.BradleyLocalThreshold(image));
        }

        [Fact]
        public void ImageFormating_BradleyLocalThreshold_NullBitmap()
        {
            //arrange
            ImageFormating imageFormating = new ImageFormating();
            //act
            //assert
            Assert.Null(imageFormating.BradleyLocalThreshold(null));
        }
    }
}