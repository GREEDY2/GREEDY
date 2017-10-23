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
    public class ImageFormating : IImageFormating
    {
        public Bitmap Blur(Bitmap bitmap, int width, int height)
        {
            Image<Gray, byte> inputImage = new Image<Gray, byte>(bitmap);
            Image<Gray, float> blurredImage = Image<Gray, float>.FromIplImagePtr(inputImage.Ptr).SmoothBlur(width, height);
            return blurredImage.Bitmap; 
        }

        //public Bitmap Resize(Bitmap bitmap, double[] cornerPoints)
        //{
        //    // define quadrilateral's corners
        //    List<IntPoint> corners = new List<IntPoint>();
        //    corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[0]), Convert.ToInt32(cornerPoints[1])));
        //    corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[2]), Convert.ToInt32(cornerPoints[3])));
        //    corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[6]), Convert.ToInt32(cornerPoints[7])));
        //    corners.Add(new IntPoint(Convert.ToInt32(cornerPoints[4]), Convert.ToInt32(cornerPoints[5])));
        //    // create filter
        //    QuadrilateralTransformation filter = new QuadrilateralTransformation(corners);
        //    // apply the filter
        //    Bitmap newImage = filter.Apply(bitmap);

        //    ImageResizeTest testWindow = new ImageResizeTest(bitmap, newImage);
        //    testWindow.Show();

        //    return newImage;
        //}

    }
}
