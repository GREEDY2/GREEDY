using System.Collections.Generic;
using System.Drawing;
using GREEDY.Extensions;
using Tesseract;

namespace GREEDY.OCRs
{
    public class TesseractOcr : IOcr
    {
        private readonly TesseractEngine _tesseract;

        private TesseractOcr()
        {
            _tesseract = new TesseractEngine
            (
                Environments.AppConfig.TesseractDataPath,
                Environments.AppConfig.OcrLanguage,
                EngineMode.TesseractOnly
            );
        }

        public List<string> ConvertImage(Bitmap image)
        {
            var page = _tesseract.Process(image);
            return page.GetLinesOfText();
        }
    }
}