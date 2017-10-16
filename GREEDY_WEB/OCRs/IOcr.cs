using System.Drawing;
using GREEDY.Models;
using Tesseract;

namespace GREEDY.OCRs
{
    public interface IOcr
    {
        Receipt ConvertImage (Pix image);
    }
}