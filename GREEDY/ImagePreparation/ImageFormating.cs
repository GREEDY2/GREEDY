using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Size = OpenCvSharp.Size;

namespace GREEDY.ImagePreparation
{
    public class ImageFormating : IImageFormating
    {
        private static readonly Mat ReceiptImage = new Mat();
        private static readonly Mat TempImage = new Mat();

        // Applies series of modifications to prepare the image for OCR reading
        public Bitmap FormatImageForOcr(Mat matImage)
        {
            ColorConversionToGray(matImage, ReceiptImage);
            ReceiptImage.CopyTo(TempImage);
            BlurAndThreshold(TempImage, 100); // parameter 100 - best selection after testing
            //ApplyErodeAndDilate(tempImage, 6); // optional; parameter 6 - best selection after testing
            CropReceiptArea(TempImage, ReceiptImage);
            return new Bitmap(ReceiptImage.ToBitmap());
        }

        private static void ColorConversionToGray(Mat fromImage, Mat toImage)
        {
            using (var gray = new Mat())
            {
                //check colors of photo
                var channels = fromImage.Channels();
                if (channels > 1)
                    Cv2.CvtColor(fromImage, gray, ColorConversionCodes.BGRA2GRAY);
                else
                    fromImage.CopyTo(gray);
                gray.CopyTo(toImage);
            }
        }

        private static void BlurAndThreshold(Mat image, int thr)
        {
            if (thr < 255)
            {
                var threshImage = new Mat();
                Cv2.Blur(image, threshImage, new Size(9, 9));
                Cv2.Threshold(threshImage, threshImage, thr, 255, ThresholdTypes.Binary);
                // construct a closing kernel and apply it to the thresholded image
                var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(21, 7));
                //var closed = new Mat();
                Cv2.MorphologyEx(image, image, MorphTypes.Close, kernel);
            }
        }

        private void ApplyErosionAndDilation(Mat image, int iter)
        {
            // perform a series of erosions and dilations
            Cv2.Erode(image, image, null, iterations: iter);
            Cv2.Dilate(image, image, null, iterations: iter);
        }

        private static void CropReceiptArea(Mat imageIn, Mat imageOut)
        {
            //find the contours in the thresholded image, then sort the contours by their area, keeping only the largest one
            Cv2.FindContours(imageIn, out var contours, out var hierarchyIndexes,
                RetrievalModes.CComp, ContourApproximationModes.ApproxSimple);

            if (contours.Length == 0) return;
            //finding biggest rectangle
            var contourIndex = 0;
            var previousArea = 0;
            var biggestContourRect = Cv2.BoundingRect(contours[0]);
            while (contourIndex >= 0)
            {
                var contour = contours[contourIndex];

                //Find bounding rectangle for each contour
                var boundingRect = Cv2.BoundingRect(contour);
                var boundingRectArea = boundingRect.Width * boundingRect.Height;
                if (boundingRectArea > previousArea)
                {
                    biggestContourRect = boundingRect;
                    previousArea = boundingRectArea;
                }

                contourIndex = hierarchyIndexes[contourIndex].Next;
            }
            //Crop the image
            imageOut = new Mat(imageOut, biggestContourRect);
        }
    }
}