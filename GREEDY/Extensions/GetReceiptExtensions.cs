using System.Collections.Generic;
using System.Linq;
using Tesseract;

namespace GREEDY.Extensions
{
    public static class GetReceiptExtensions
    {
        public static List<string> GetLinesOfText(this Emgu.CV.OCR.Tesseract source) => source.GetUTF8Text()?.Split('\n').ToList();

        public static List<string> GetLinesOfText(this Page source) => source.GetText().Split('\n').ToList();
    }
}