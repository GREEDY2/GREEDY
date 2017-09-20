using System.Linq;
using GREEDY.Interfaces;
using GREEDY.Models;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.OCR;
using System.Configuration;

namespace GREEDY.Controllers
{
    public class EmguOCR : iOCR
    {
        private static Emgu.CV.OCR.Tesseract tess1 = new Emgu.CV.OCR.Tesseract(
            "", ConfigurationManager.AppSettings["languageOCR"], OcrEngineMode.Default); 
        // this looks weird because the keyword Tesseract is used in another nuget package

        public Receipt UseOCR(string url)
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>(url);
            Receipt receipt = new Receipt();
            tess1.SetImage(img);
            tess1.Recognize();
            receipt.LinesOfText = tess1.GetUTF8Text().Split('\n').ToList();
            return receipt;
        }
    }
}
