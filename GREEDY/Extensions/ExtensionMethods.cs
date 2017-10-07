using System.Linq;
using GREEDY.Models;
using Tesseract;

namespace GREEDY.Extensions
{
    public static class ExtensionMethods
    {
        public static Receipt GetReceipt(this Emgu.CV.OCR.Tesseract source)
        {
            var linesOfText = source.GetUTF8Text()?.Split('\n').ToList();
            return new Receipt { LinesOfText = linesOfText };
        }

        public static Receipt GetReceipt(this Page source)
        {
            return new Receipt
            {
                PercentageMatched = source.GetMeanConfidence(),
                LinesOfText = source.GetText().Split('\n').ToList()
            };
        }
    }
}