using System;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;
using GREEDY.DataManagers;

namespace GREEDY.ImagePreparation
{
    public class ImageFormating : IImageFormating
    {
        /// <summary>
        /// Applies series of modifications to prepare the image for OCR reading
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public Bitmap FormatImage(Bitmap bitmap)
        {
            Bitmap edited;
            edited = Binarization(bitmap);
            edited = BiggestBlob(edited);
            edited = Rotate(edited);
            edited = Deskew(edited);
            edited = BiggestBlob(edited);
            edited = Rescale(edited);
            return edited;
        }

        //Not really sure what it does but the recomendations suggest it helps the OCR read better
        private Bitmap Rescale(Bitmap bitmap)
        {
            Bitmap rescaledBitmap = new Bitmap(bitmap);
            rescaledBitmap.SetResolution(300, 300); //recomended dpi for OCR
            return rescaledBitmap;
        }

        /// <summary>
        /// Deskew finds the text lines and rotates them so they would be horizontal
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private Bitmap Deskew(Bitmap bitmap)
        {
            Deskew deskew = new Deskew();
            Bitmap deskewdImage = deskew.DeskewImage(bitmap);
            return deskewdImage;
        }

        /// <summary>
        /// Rotates the image if its width is more than its height
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private Bitmap Rotate(Bitmap bitmap)
        {
            Bitmap rotatedBitmap = new Bitmap(bitmap);
            if (bitmap.Height < bitmap.Width)
            {
                rotatedBitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            return rotatedBitmap;
        }

         
        /// <summary>
        /// Finds the biggest blob (biggest area of one color)
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private Bitmap BiggestBlob(Bitmap bitmap)
        {
            ExtractBiggestBlob filter = new ExtractBiggestBlob();
            Bitmap edited = filter.Apply(bitmap);
            IntPoint blobPosition = filter.BlobPosition;
            Rectangle cropArea = new Rectangle(blobPosition.X, blobPosition.Y, edited.Width, edited.Height);
            edited = cropImage(bitmap, cropArea);
            return edited;
        }

        private Bitmap cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }
        
        /// <summary>
        /// Turns the image to only black and white
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private Bitmap Binarization(Bitmap bitmap)
        {
            Image<Gray, Byte> img = new Image<Gray, byte>(bitmap);
            img = img.ThresholdBinary(new Gray(145), new Gray(255));//magic numbers (most optimal values)
            return img.Bitmap;
        }
    }
}
