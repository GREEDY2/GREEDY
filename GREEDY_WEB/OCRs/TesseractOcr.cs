using System.Drawing;
using GREEDY.Extensions;
using GREEDY.Models;
using Tesseract;

namespace GREEDY.OCRs
{
    public class TesseractOcr : IOcr
    {
        private TesseractEngine _tesseract;

        public Receipt ConvertImage(Bitmap image)
        {
            _tesseract = new TesseractEngine
            (
                Environments.AppConfig.TesseractDataPath,
                Environments.AppConfig.OcrLanguage,
                EngineMode.TesseractOnly
            );
            var page = _tesseract.Process(image);
            return page.GetReceipt();
        }
    }
}