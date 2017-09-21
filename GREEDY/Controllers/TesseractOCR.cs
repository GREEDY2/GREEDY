using GREEDY.Interfaces;
using GREEDY.Models;
using System.Configuration;
using System.Drawing;
using System.Linq;
using Tesseract;

namespace GREEDY.Controllers
{
    public class TesseractOCR : iOCR
    {
        public Receipt UseOCR(string url)
        {
            string tessData = ConfigurationManager.AppSettings["tessData"];
            string languageOCR = ConfigurationManager.AppSettings["languageOCR"];
            var image = new Bitmap(url);
            Receipt receipt = new Receipt();
            var ocr = new TesseractEngine(tessData, languageOCR, EngineMode.TesseractAndCube);
            var page = ocr.Process(image);
            receipt.PercentageMatched = page.GetMeanConfidence();
            receipt.LinesOfText = page.GetText().Split('\n').ToList();
            return receipt;
        }
    }
}
