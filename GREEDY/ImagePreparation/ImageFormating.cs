using System;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;
using GREEDY.OCRs;
using System.Collections.Generic;

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
                Bitmap edited = new Bitmap(Binarization(new Bitmap(bitmap)));
                //edited = RemoveNoise(edited);
                edited = BiggestBlob(new Bitmap(edited));
                edited = Rotate(new Bitmap(bitmap));
                edited = _deskewImage.Deskew(new Bitmap(edited));
                //edited = BiggestBlob(edited);
                edited = Rescale(new Bitmap(edited));
                return new Bitmap(bitmap);
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
        //fail for all image
        public Bitmap RemoveNoise(Bitmap bitmap)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(bitmap);
            Image<Bgr, byte> edited = image.SmoothMedian(7);
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
