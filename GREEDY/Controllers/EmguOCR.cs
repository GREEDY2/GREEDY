using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEDY.Interfaces;
using GREEDY.Models;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.OCR;

namespace GREEDY.Controllers
{
    public class EmguOCR : iOCR
    {
        private static Emgu.CV.OCR.Tesseract tess1 = new Emgu.CV.OCR.Tesseract("", "eng", OcrEngineMode.Default); 
        // this looks weird because the keyword Tesseract is used in another nuget package

        // example of Image initialization
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
