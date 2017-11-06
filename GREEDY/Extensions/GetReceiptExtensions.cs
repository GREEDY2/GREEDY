using System.Linq;
using Tesseract;
using System.Collections.Generic;

namespace GREEDY.Extensions
{
    public static class GetReceiptExtensions
    {
        public static List<string> GetLinesOfText(this Emgu.CV.OCR.Tesseract source)
        {
            var linesOfText = source.GetUTF8Text()?.Split('\n').ToList();
            return linesOfText;
        }

        public static List<string> GetLinesOfText(this Page source)
        {
            var linesOfText = source.GetText().Split('\n').ToList();
            return linesOfText;
        }
    }
}