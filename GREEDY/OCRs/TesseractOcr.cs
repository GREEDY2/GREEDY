﻿using System.Drawing;
using GREEDY.Extensions;
using Tesseract;
using System.Collections.Generic;

namespace GREEDY.OCRs
{
    public class TesseractOcr : IOcr
    {
        private TesseractEngine _tesseract;

        TesseractOcr()
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