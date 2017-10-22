using System;
using System.Collections.Generic;
using System.Drawing;
using AForge;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;
using GREEDY.View;

namespace GREEDY.Services
{
    class ImageFormatService : IImageFormatService
    {
        public ImageFormatService()
        {
        }

        public Bitmap BlackAndWhite(Bitmap bitmap)
        {
            int threshold_value = 150;
            Image<Gray, Byte> blackAndWhiteImage = new Image<Gray, Byte>(bitmap);
            blackAndWhiteImage = blackAndWhiteImage.ThresholdBinary(new Gray(threshold_value), new Gray(255));

            return blackAndWhiteImage.Bitmap;
        }

        public Bitmap Blur(Bitmap bitmap, int width, int height)
        {
            Image<Gray, byte> inputImage = new Image<Gray, byte>(bitmap);
            Image<Gray, float> blurredImage = Image<Gray, float>.FromIplImagePtr(inputImage.Ptr).SmoothBlur(width, height);
            return blurredImage.Bitmap; 
        }

        public Bitmap Dilate(Bitmap bitmap, int iterations)
        {
            Image<Gray, float> inputImage = new Image<Gray, float>(bitmap);
            Image<Gray, float> dilatedImage = Image<Gray, float>.FromIplImagePtr(inputImage.Ptr).Dilate(1);
            return dilatedImage.Bitmap;
        }

        public Bitmap Erode(Bitmap bitmap, int iterations)
        {
            Image<Gray, float> inputImage = new Image<Gray, float>(bitmap);
            Image<Gray, float> erodedImage = Image<Gray, float>.FromIplImagePtr(inputImage.Ptr).Erode(1);
            return erodedImage.Bitmap;
        }

       public Bitmap Resize(Bitmap bitmap, double[] cornerPoints)
        {
            // define quadrilateral's corners
            List<IntPoint> corners = new List<IntPoint>();
            corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[0]), Convert.ToInt32(cornerPoints[1])));
            corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[2]), Convert.ToInt32(cornerPoints[3])));
            corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[6]), Convert.ToInt32(cornerPoints[7])));
            corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[4]), Convert.ToInt32(cornerPoints[5])));
            // create filter
            QuadrilateralTransformation filter = new QuadrilateralTransformation(corners);
            // apply the filter
            Bitmap newImage = filter.Apply(bitmap);

            ImageResizeTest testWindow = new ImageResizeTest(bitmap, newImage);
            testWindow.Show();

            return newImage;
        }

    }
}
