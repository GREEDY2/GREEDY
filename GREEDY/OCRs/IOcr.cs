using System.Drawing;
using System.Collections.Generic;

namespace GREEDY.OCRs
{
    public interface IOcr
    {
        List<string> ConvertImage (Bitmap image);
    }
}