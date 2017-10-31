using System;
using System.Collections.Generic;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Point = System.Drawing.Point;
using MoreLinq;
using System.Linq;

namespace GREEDY.DataManagers
{
    public class ImageFormating : IImageFormating
    {
        // adds BradleyLocalThreshold filter to the picture
        private Bitmap BradleyLocalThreshold(Bitmap bitmap) 
        {
            Image<Gray, byte> inputImage = new Image<Gray, byte>(bitmap);
            BradleyLocalThresholding bradleyLocalThresholding = new BradleyLocalThresholding();
            Bitmap filtered = new Bitmap(bradleyLocalThresholding.Apply(inputImage.Bitmap));
            return filtered;
        }

        private Bitmap Crop(Bitmap bitmap, double[] cornerPoints)
        {
            // define quadrilateral's corners
            List<IntPoint> corners = new List<IntPoint>
            {
                new IntPoint(Convert.ToInt32(cornerPoints[0]), Convert.ToInt32(cornerPoints[1])),
                new IntPoint(Convert.ToInt32(cornerPoints[2]), Convert.ToInt32(cornerPoints[3])),
                new IntPoint(Convert.ToInt32(cornerPoints[4]), Convert.ToInt32(cornerPoints[5])),
                new IntPoint(Convert.ToInt32(cornerPoints[6]), Convert.ToInt32(cornerPoints[7]))
            };
            QuadrilateralTransformation filter = new QuadrilateralTransformation(corners);
            Bitmap newImage = filter.Apply(bitmap);
            return newImage;
        }

        //returns the bigest found rectangle
        private Bitmap FilterCropedImages(List<Bitmap> list)
        {
            if (list.Any())
                return list.MaxBy(x => x.Height * x.Width); // this guy throws an exception if list is null.
            else
                return null;
        }

        public Bitmap FormatImage(Bitmap bitmap)
        {
            if (bitmap.Width > bitmap.Height)
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

            int originalWidth = bitmap.Width;
            int originalHeight = bitmap.Height;

            Image<Bgr, Byte> img =
                new Image<Bgr, byte>(bitmap).Resize(400, 400, Inter.Linear, true); //resizing is needed for better rectangle detection

            int resizedWidth = img.Width;
            int resizedHeight = img.Height;

            //Convert the image to grayscale and filter out the noise
            UMat uimage = new UMat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);

            //use image pyr to remove noise
            UMat pyrDown = new UMat();
            CvInvoke.PyrDown(uimage, pyrDown);
            CvInvoke.PyrUp(pyrDown, uimage);

            // These values work best
            double cannyThreshold = 180.0;
            double cannyThresholdLinking = 120.0;
            UMat cannyEdges = new UMat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);

            List<Bitmap> cropedImagesList = new List<Bitmap>();

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (CvInvoke.ContourArea(approxContour, false) > (resizedHeight*resizedWidth)/3)//only consider contours with area greater than the third of the whole image
                        {
                            if (approxContour.Size == 4) //The contour has 4 vertices.
                            {
                                //determine if all the angles in the contour are within [70, 110] degree
                                bool isRectangle = true;
                                Point[] pts = approxContour.ToArray();
                                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                for (int j = 0; j < edges.Length; j++)
                                {
                                    double angle = Math.Abs(
                                       edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                    if (angle < 70 || angle > 110) // these values mean that the angle must be a right angle
                                    {
                                        isRectangle = false;
                                        break;
                                    }
                                }

                                if (isRectangle)
                                {
                                    double[] corners = new double[8];
                                    for (int j = 0; j < 4; j++)
                                    {
                                        corners[2 * j] = Convert.ToDouble(approxContour[j].X) * originalWidth / resizedWidth;
                                        corners[2 * j + 1] = Convert.ToDouble(approxContour[j].Y) * originalHeight / resizedHeight;
                                    }

                                    //crop only if X1 is to the left of X2
                                    if (corners[0] <= corners[2])
                                        cropedImagesList.Add(Crop(bitmap, corners));
                                }
                            }
                        }
                    }
                }
            }

            if (FilterCropedImages(cropedImagesList) != null) //if we crop something
            {
                //crop image and add filter
                var result = FilterCropedImages(cropedImagesList);
                result = BradleyLocalThreshold(result);

                if (result.Width > result.Height)
                {
                    result.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    return result;
                }
                return result;
            }
            else
            {
                //add only filter on original image
                var result = BradleyLocalThreshold(bitmap);
                return result;
            }
        }
    }
}
