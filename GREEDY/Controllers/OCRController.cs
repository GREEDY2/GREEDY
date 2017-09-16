using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEDY.Models;
using System.Drawing;
using Tesseract;

namespace GREEDY.Controllers
{
    public class OCRController
    {
        private Receipt Receipt;
        private Bitmap Image;

        public OCRController (Receipt model, Bitmap image)
        {
            this.Receipt = model;
            this.Image = image;
        }

        public void UseOCR()
        {
            var ocr = new TesseractEngine("../../../Data/tessdata", "eng", EngineMode.TesseractAndCube);
            var page = ocr.Process(Image);
            //Console.WriteLine(page.GetMeanConfidence());
            //Receipt.RawText = page.GetText();
            Receipt.LinesOfText = page.GetText().Split('\n').ToList();
        }
    }
}
