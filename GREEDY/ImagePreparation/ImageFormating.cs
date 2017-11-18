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
        private static DeskewImage DeskewImage => new DeskewImage();

        /// Applies series of modifications to prepare the image for OCR reading
        public Bitmap FormatImage(Bitmap bitmap)
        {
            try
            {
                Bitmap edited = Binarization(bitmap);
                edited = RemoveNoise(edited);
                edited = BiggestBlob(edited);
                edited = Rotate(edited);
                edited = DeskewImage.Deskew(edited);
                edited = BiggestBlob(edited);
                edited = Rescale(edited);
                return edited;
            }
            catch
            {
                return bitmap;
            }
        }

        // Turns the image to only black and white
        public Bitmap Binarization(Bitmap bitmap)
        {
            Image<Gray, Byte> img = new Image<Gray, byte>(bitmap);
            img = img.ThresholdBinary(new Gray(145), new Gray(255)); //magic numbers (most optimal values)
            return img.Bitmap;
        }

        // Removes noise (small dots/smudges from an image)
        public Bitmap RemoveNoise(Bitmap bitmap)
        {
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
            Image<Gray, byte> edited = image.SmoothMedian(7);
            return edited.ToBitmap();
        }

        // Finds the biggest area of one color
        public Bitmap BiggestBlob(Bitmap bitmap)
        {
            try
            {
                ExtractBiggestBlob filter = new ExtractBiggestBlob();
                Bitmap edited = filter.Apply(bitmap);
                IntPoint blobPosition = filter.BlobPosition;
                Rectangle cropArea = new Rectangle(blobPosition.X, blobPosition.Y, edited.Width, edited.Height);
                edited = CropImage(bitmap, cropArea);
                return edited;
            }
            catch
            {
                return bitmap;
            }
        }

        public Bitmap CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
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

        //Rescale helps the OCR read better
        public Bitmap Rescale(Bitmap bitmap)
        {
            bitmap.SetResolution(300, 300); //recomended DPI for OCR
            return bitmap;
        }
    }
}
