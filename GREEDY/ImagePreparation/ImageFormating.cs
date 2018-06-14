using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace GREEDY.ImagePreparation
{
    public class ImageFormating : IImageFormating
    {
        private static Mat receiptImage = new Mat();

        // Applies series of modifications to prepare the image for OCR reading
        public Bitmap FormatImage(Mat matImage)
        {
            ColorConversionToGray(matImage);
            Crop_BiggestBlob(receiptImage);
            //TODO:

            return new Bitmap(BitmapConverter.ToBitmap(receiptImage));
        }

        private void ColorConversionToGray(Mat image)
        {
            using (var gray = new Mat())
            {
                //check colors of photo
                var channels = image.Channels();
                if (channels > 1)
                {
                    Cv2.CvtColor(image, gray, ColorConversionCodes.BGRA2GRAY);
                }
                else
                {
                    image.CopyTo(gray);
                }
                gray.CopyTo(receiptImage);
            }
        }

        private void Crop_BiggestBlob(Mat image)
        {
            using (Mat template = new Mat())
            {
                // blur and threshold the image
                // parameter 100 - best selection after testing
                Cv2.Blur(image, template, new OpenCvSharp.Size(9, 9));
                Cv2.Threshold(template, template, thresh: 100, maxval: 255, type: ThresholdTypes.Binary);

                // perform a series of erosions and dilations
                // parameter 6 - best selection after testing
                Cv2.Erode(template, template, null, iterations: 6);
                Cv2.Dilate(template, template, null, iterations: 6);

                // construct a closing kernel and apply it to the thresholded image
                var kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(21, 7));
                Cv2.MorphologyEx(template, template, MorphTypes.Close, kernel);

                //find the contours in the thresholded image, then sort the contours
                //by their area, keeping only the largest one
                Cv2.FindContours(template, out OpenCvSharp.Point[][] contours, out HierarchyIndex[] hierarchyIndexes,
                    mode: RetrievalModes.CComp, method: ContourApproximationModes.ApproxSimple);

                if (contours.Length != 0)
                {
                    //finding bigest rectangle
                    var contourIndex = 0;
                    var previousArea = 0;
                    var biggestContourRect = Cv2.BoundingRect(contours[0]);
                    while ((contourIndex >= 0))
                    {
                        var contour = contours[contourIndex];

                        //Find bounding rect for each contour
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
                    new Mat(image, biggestContourRect).CopyTo(receiptImage);
                }
            }
        }











        //////////////////////////////Rescale helps the OCR read better
        ////////////////////////////public Bitmap Rescale(Bitmap bitmap)
        ////////////////////////////{
        ////////////////////////////    if (bitmap.HorizontalResolution < 300 && bitmap.VerticalResolution < 300)
        ////////////////////////////    {
        ////////////////////////////        bitmap.SetResolution(300, 300);
        ////////////////////////////    }
        ////////////////////////////    return bitmap;
        ////////////////////////////}







        ////////////////////////////// Rotates the image if its width is more than its height
        ////////////////////////////public Bitmap Rotate(Bitmap bitmap)
        ////////////////////////////{
        ////////////////////////////    if (bitmap.Height < bitmap.Width)
        ////////////////////////////    {
        ////////////////////////////        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        ////////////////////////////    }
        ////////////////////////////    return bitmap;
        ////////////////////////////}

        ////////////////////////////// Turns the image to only black and white
        ////////////////////////////public Bitmap Binarization(Bitmap bitmap)
        ////////////////////////////{
        ////////////////////////////    //Image<Gray, Byte> img = new Image<Gray, byte>(bitmap);
        ////////////////////////////    //img = img.ThresholdBinary(new Gray(145), new Gray(255)); //magic numbers (most optimal values)
        ////////////////////////////    //return img.Bitmap;

        ////////////////////////////    //Threshold filter = new Threshold(100);
        ////////////////////////////    //return filter.Apply(bitmap);


        ////////////////////////////    //create a blank bitmap the same size as original
        ////////////////////////////    Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height, Graphics.FromImage(bitmap));

        ////////////////////////////    //get a graphics object from the new image
        ////////////////////////////    Graphics g = Graphics.FromImage(newBitmap);

        ////////////////////////////    //create the grayscale ColorMatrix
        ////////////////////////////    ColorMatrix colorMatrix = new ColorMatrix(
        ////////////////////////////       new float[][]
        ////////////////////////////       {
        ////////////////////////////         new float[] {.3f, .3f, .3f, 0, 0},
        ////////////////////////////         new float[] {.59f, .59f, .59f, 0, 0},
        ////////////////////////////         new float[] {.11f, .11f, .11f, 0, 0},
        ////////////////////////////         new float[] {0, 0, 0, 1, 0},
        ////////////////////////////         new float[] {0, 0, 0, 0, 1}
        ////////////////////////////       });

        ////////////////////////////    //create some image attributes
        ////////////////////////////    ImageAttributes attributes = new ImageAttributes();

        ////////////////////////////    //set the color matrix attribute
        ////////////////////////////    attributes.SetColorMatrix(colorMatrix);

        ////////////////////////////    //draw the original image on the new image using the grayscale color matrix
        ////////////////////////////    g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
        ////////////////////////////       0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attributes);

        ////////////////////////////    //dispose the Graphics object
        ////////////////////////////    g.Dispose();
        ////////////////////////////    return newBitmap;
        ////////////////////////////}

        ////////////////////////////// Removes noise (small dots/smudges from an image)
        //////////////////////////////fail for all image
        ////////////////////////////public Bitmap RemoveNoise(Bitmap bitmap)
        ////////////////////////////{
        ////////////////////////////    try
        ////////////////////////////    {
        ////////////////////////////        Median filter = new Median();
        ////////////////////////////        return filter.Apply(bitmap);
        ////////////////////////////    }
        ////////////////////////////    catch
        ////////////////////////////    {
        ////////////////////////////        return bitmap;
        ////////////////////////////    }



        ////////////////////////////    //// create filter

        ////////////////////////////    //// apply the filter
        ////////////////////////////    //filter.ApplyInPlace(bitmap);


        ////////////////////////////    //Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
        ////////////////////////////    //Image<Gray, byte> edited = image.SmoothMedian(7);
        ////////////////////////////    //return edited.ToBitmap();
    }
}