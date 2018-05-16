using System;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;

namespace GREEDY.ImagePreparation
{
    public class ImageFormating : IImageFormating
    {
        private readonly DeskewImage _deskewImage;

        public ImageFormating()
        {
            _deskewImage = new DeskewImage();
        }
        // Applies series of modifications to prepare the image for OCR reading
        public Bitmap FormatImage(Bitmap bitmap)
        {
            try
            {
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Bitmap edited = new Bitmap(bitmap);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                edited = Rescale(edited);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                edited = BiggestBlob(edited);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                edited = Rotate(edited);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                edited = _deskewImage.Deskew(edited);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                edited = Binarization(edited);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                edited = RemoveNoise(edited);
                Console.WriteLine("resolution: {0}, {1}", bitmap.HorizontalResolution, bitmap.VerticalResolution);
                Console.WriteLine("resolution: {0}, {1}", edited.HorizontalResolution, edited.VerticalResolution);
                return edited;
            }
            catch
            {
                return bitmap;
            }
        }
        //Rescale helps the OCR read better
        public Bitmap Rescale(Bitmap bitmap)
        {
            if (bitmap.HorizontalResolution < 300 && bitmap.VerticalResolution < 300)
            {
                bitmap.SetResolution(300, 300);
            }
            return bitmap;
        }

        // Finds the biggest area of one color
        public Bitmap BiggestBlob(Bitmap bitmap)
        {
            try
            {
                ExtractBiggestBlob filter = new ExtractBiggestBlob();
                Bitmap newImage = filter.Apply(new Bitmap(bitmap));
                IntPoint blobPosition = filter.BlobPosition;
                Rectangle cropArea = new Rectangle(blobPosition.X, blobPosition.Y, newImage.Width, newImage.Height);
                return newImage.Clone(cropArea, newImage.PixelFormat);
            }
            catch
            {
                return bitmap;
            }
        }

        // Rotates the image if its width is more than its height
        public Bitmap Rotate(Bitmap bitmap)
        {
            if (bitmap.Height < bitmap.Width)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            return bitmap;
        }

        // Turns the image to only black and white
        public Bitmap Binarization(Bitmap bitmap)
        {
            //Image<Gray, Byte> img = new Image<Gray, byte>(bitmap);
            //img = img.ThresholdBinary(new Gray(145), new Gray(255)); //magic numbers (most optimal values)
            //return img.Bitmap;

            Threshold filter = new Threshold(100);
            return filter.Apply(bitmap);
        }

        // Removes noise (small dots/smudges from an image)
        //fail for all image
        public Bitmap RemoveNoise(Bitmap bitmap)
        {
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
            Image<Gray, byte> edited = image.SmoothMedian(7);
            return edited.ToBitmap();
        }


    }
}
