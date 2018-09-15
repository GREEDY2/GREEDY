using System.Collections.Generic;
using System.Drawing;

namespace GREEDY.OCRs
{
    public interface IOcr
    {
        List<string> ConvertImage(Bitmap image);
    }
}