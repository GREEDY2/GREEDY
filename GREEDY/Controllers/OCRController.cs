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
        public Receipt UseOCR(Bitmap image)
        {
            Receipt receipt = new TesseractOCR().UseOCR(image);
            return receipt;
        }

        //Lukui
        /*public Receipt UseOCR(kazkoksTipas image)
        {
            //Luko kodas
            return receipt;
        }*/
    }
}
