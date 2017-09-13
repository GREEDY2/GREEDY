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
        private Receipt receipt;
        private Bitmap image;

        public OCRController (Receipt model, Bitmap image)
        {
            this.receipt = model;
            this.image = image;
        }

        public void UseOCR()
        {
            var ocr = new TesseractEngine("../../../Data/tessdata", "eng", EngineMode.TesseractAndCube);
            var page = ocr.Process(image);
            receipt.rawText = page.GetText();
        }
    }
}
