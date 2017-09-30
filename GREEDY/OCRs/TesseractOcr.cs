using System.Drawing;
using GREEDY.Extensions;
using GREEDY.Models;
using Tesseract;

namespace GREEDY.OCRs
{
    // currently doesn't work cause the nuget is shit
    // can add dll's to make it work but still...
    public class TesseractOcr : IOcr
    {
        private readonly TesseractEngine _tesseract;
        private readonly IAppConfig _config;
        
        public TesseractOcr (IAppConfig config)
        {
            _config = config;
            _tesseract = new TesseractEngine 
            (
                _config.TesseractDataPath,
                _config.OcrLanguage,
                EngineMode.TesseractAndCube
            );
        }
        
        public Receipt ConvertImage (Bitmap image)
        {
            var page = _tesseract.Process(image);
            return page.GetReceipt ();
        }
    }
}