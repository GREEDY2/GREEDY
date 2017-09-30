using System.Drawing;
using GREEDY.Extensions;
using GREEDY.Models;
using Tesseract;

namespace GREEDY.OCRs
{
    public class TesseractOcr : IOcr
    {
        private readonly TesseractEngine _tesseract;

        public TesseractOcr()
        {
            _tesseract = new TesseractEngine
            (
                Environment.AppConfig.TesseractDataPath,
                Environment.AppConfig.OcrLanguage,
                EngineMode.TesseractAndCube
            );
        }

        public Receipt ConvertImage(Bitmap image)
        {
            var page = _tesseract.Process(image);
            return page.GetReceipt();
        }
    }
}