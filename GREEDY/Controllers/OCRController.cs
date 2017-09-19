using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GREEDY.Models;
using System.Drawing;
using Tesseract;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.OCR;

namespace GREEDY.Controllers
{
    public class OCRController
    {
        public Receipt UseOCR(Bitmap image)
        {
            Receipt receipt = new TesseractOCR().UseOCR(image);
            return receipt;
        }

        public Receipt UseOCR(Image<Bgr, byte> image)
        {
            Receipt receipt = new EmguOCR().UseOCR(image);
            return receipt;
        }
    }
}
