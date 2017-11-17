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
            var PercentageMatched = source.GetMeanConfidence();
            if (PercentageMatched > 0.5)
            {
                var linesOfText = source.GetText().Split('\n').ToList();
                //added PercentageMatched in the end of the list
                linesOfText.Add(PercentageMatched.ToString());
                return linesOfText;
            }
            else
            {
                return new List<string> { PercentageMatched.ToString() };
            }
        }
    }
}