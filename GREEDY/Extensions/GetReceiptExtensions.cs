using System.Linq;
using System.Collections.Generic;
using Tesseract;

namespace GREEDY.Extensions
{
    public static class GetReceiptExtensions
    {
        public static List<string> GetLinesOfText(this Emgu.CV.OCR.Tesseract source)
        {
            return source.GetUTF8Text()?.Split('\n').ToList();
        }

        public static List<string> GetLinesOfText(this Page source)
        {
            return source.GetText().Split('\n').ToList();
        }
    }
}