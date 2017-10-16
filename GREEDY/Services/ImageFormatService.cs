using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public Bitmap Blur(Bitmap bitmap, int width, int height)
        {
            Image<Gray, float> inputImage = new Image<Gray, float>(bitmap);
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
        
    }
}
