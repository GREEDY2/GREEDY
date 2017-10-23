using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Services
{
    public interface IImageFormating
    {
        Bitmap Blur(Bitmap bitmap, int width, int height);
       // Bitmap Resize(Bitmap bitmap, double[] cornerPoints);
    }
}
