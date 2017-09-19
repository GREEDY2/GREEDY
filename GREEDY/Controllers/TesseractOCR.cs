using GREEDY.Interfaces;
using GREEDY.Models;
using System.Drawing;
using System.Linq;
using Tesseract;

namespace GREEDY.Controllers
{
    public class TesseractOCR
    {
        public Receipt UseOCR(Bitmap image)
        {
            Receipt receipt = new Receipt();
           /* var ocr = new Tesseract.TesseractEngine("../../../Data/tessdata", "eng", EngineMode.TesseractAndCube);
            var page = ocr.Process(image);
            receipt.PercentageMatched = page.GetMeanConfidence();
            receipt.LinesOfText = page.GetText().Split('\n').ToList();*/
            return receipt;
        }
    }
}
