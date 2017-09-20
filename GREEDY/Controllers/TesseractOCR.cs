using GREEDY.Interfaces;
using GREEDY.Models;
using System.Drawing;
using System.Linq;
using Tesseract;

namespace GREEDY.Controllers
{
    public class TesseractOCR : iOCR
    {
        public Receipt UseOCR(string url)
        {
            var image = new Bitmap(url);
            Receipt receipt = new Receipt();
            var ocr = new Tesseract.TesseractEngine("../../../Data/tessdata", "eng", EngineMode.TesseractAndCube);
            var page = ocr.Process(image);
            receipt.PercentageMatched = page.GetMeanConfidence();
            receipt.LinesOfText = page.GetText().Split('\n').ToList();
            return receipt;
        }
    }
}
