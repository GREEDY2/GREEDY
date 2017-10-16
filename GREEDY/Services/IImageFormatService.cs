using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Services
{
    interface IImageFormatService
    {
        Bitmap Blur(Bitmap bitmap, int width, int height);
        Bitmap Dilate(Bitmap bitmap, int iterations);
        Bitmap Erode(Bitmap bitmap, int iterations);
    }
}
