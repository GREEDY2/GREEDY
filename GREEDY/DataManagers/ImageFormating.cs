using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

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
    }
}
