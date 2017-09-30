using System.Drawing;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using GREEDY.Models;
using GREEDY.Extensions;

namespace GREEDY.OCRs
{
    public class EmguOcr : IOcr
    {
        private readonly Emgu.CV.OCR.Tesseract _tesseract;
        private readonly IAppConfig _config;

        public EmguOcr (IAppConfig config)
        {
            _config = config;
            _tesseract = new Emgu.CV.OCR.Tesseract
            (
                _config.TesseractDataPath,
                _config.OcrLanguage,
                OcrEngineMode.Default
            );
        }
        
        public Receipt ConvertImage (Bitmap image)
        {
            _tesseract.SetImage (new Image<Bgr, byte> (image));
            _tesseract.Recognize ();
            return _tesseract.GetReceipt ();
        }
    }
}