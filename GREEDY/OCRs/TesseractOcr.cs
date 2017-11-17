using System.Drawing;
using GREEDY.Extensions;
using Tesseract;
using System.Collections.Generic;

namespace GREEDY.OCRs
{
    public class TesseractOcr : IOcr
    {
        private TesseractEngine _tesseract;

        public List<string> ConvertImage(Bitmap image)
        {
            _tesseract = new TesseractEngine
            (
                Environments.AppConfig.TesseractDataPath,
                Environments.AppConfig.OcrLanguage,
                EngineMode.TesseractOnly
            );
            var page = _tesseract.Process(image);
            
            return page.GetLinesOfText();
        }
    }
}