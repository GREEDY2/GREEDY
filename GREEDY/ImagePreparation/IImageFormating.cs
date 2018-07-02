using System.Drawing;
using OpenCvSharp;

namespace GREEDY.ImagePreparation
{
    public interface IImageFormating
    {
        Bitmap FormatImageForOcr(Mat bitmap);
    }
}