//using System.Drawing;
//using GREEDY.ImagePreparation;
//using Xunit;
//using System;

//namespace GREEDY.UnitTests.ImagePreparationUnitTest
//{
//    public class UnitTest_ImageFormating
//    {

//        [Fact]
//        public void ImageFormating_Binarization_NullBitmap()
//        {
//            //arrange
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Throws<NullReferenceException>(() => imageFormating.Binarization(null));
//        }

//        [Fact]
//        public void ImageFormating_RemoveNoise_NullBitmap()
//        {
//            //arrange
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Throws<NullReferenceException>(() => imageFormating.RemoveNoise(null));
//        }

//        [Fact]
//        public void ImageFormating_BiggestBlob_ImageWithoutBlob()
//        {
//            //arrange
//            Bitmap image = new Bitmap(50,100);
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Equal(image, imageFormating.BiggestBlob(image));
//        }

//        [Fact]
//        public void ImageFormating_BiggestBlob_OnePixelImage()
//        {
//            //arrange
//            Bitmap image = new Bitmap(1, 1);
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Equal(image, imageFormating.BiggestBlob(image));
//        }

//        [Fact]
//        public void ImageFormating_BiggestBlob_NullBitmap()
//        {
//            //arrange
//            Bitmap image = null;
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Equal(image, imageFormating.BiggestBlob(image));
//        }

//        [Fact]
//        public void ImageFormating_Rotate_RotateGivenBitmap1()
//        {
//            //arrange
//            ImageFormating imageFormating = new ImageFormating();
//            Bitmap image = new Bitmap(300, 1);
//            //act
//            //assert
//            Assert.Equal(image.Width, imageFormating.Rotate(image).Height);
//        }

//        [Fact]
//        public void ImageFormating_Rotate_RotateGivenBitmap2()
//        {
//            //arrange
//            ImageFormating imageFormating = new ImageFormating();
//            Bitmap image = new Bitmap(1, 300);
//            //act
//            //assert
//            Assert.Equal(image.Size, imageFormating.Rotate(image).Size);
//        }

//        [Fact]
//        public void ImageFormating_Rotate_NullBitmap()
//        {
//            //arrange
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Throws<NullReferenceException>(() => imageFormating.Rotate(null));
//        }

//        [Fact]
//        public void ImageFormating_Rescale_RescaleGivenBitmap()
//        {
//            //arrange
//            Bitmap image = new Bitmap(9, 1);
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Equal(image, imageFormating.Rescale(image));
//        }

//        [Fact]
//        public void ImageFormating_Rescale_NullBitmap()
//        {
//            //arrange
//            ImageFormating imageFormating = new ImageFormating();
//            //act
//            //assert
//            Assert.Throws<NullReferenceException>(() => imageFormating.Rescale(null));
//        }

//        [Fact]
//        public void DeskewImage_Deskew_NullBitmap()
//        {
//            //arrange
//            DeskewImage deskewImage = new DeskewImage();
//            //act
//            //assert
//            Assert.Throws<NullReferenceException>(() => deskewImage.Deskew(null));
//        }
//    }
//}