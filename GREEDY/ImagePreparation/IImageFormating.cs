using OpenCvSharp;
using System.Drawing;

namespace GREEDY.ImagePreparation
{
    public interface IImageFormating
    {
        Bitmap FormatImageForOCR(Mat bitmap);
    }
}
