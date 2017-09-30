using System.Drawing;
using GREEDY.Models;

namespace GREEDY.OCRs
{
    public interface IOcr
    {
        Receipt ConvertImage (Bitmap image);
    }
}