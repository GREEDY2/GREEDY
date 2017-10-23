using System.Collections.Generic;
using GREEDY.DataManagers;
using GREEDY.Models;
using GREEDY.OCRs;
using System.Drawing;
using System;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IImageFormating _imageFormating;
        private readonly IOcr _ocr;
        private readonly IDataConverter _dataConverter;
        private readonly IDataManager _dataManager;

        public ReceiptService(IImageFormating imageFormating, IOcr ocr, IDataConverter dataConverter, IDataManager dataManager)
        {
            _imageFormating = imageFormating;
            _ocr = ocr;
            _dataConverter = dataConverter;
            _dataManager = dataManager;
        }

        public List<Item> ProcessReceiptImage(Bitmap image)
        {
            if (image != null)
            {
                var imageNew = _imageFormating.Blur(image, 5, 5); // 5 and 5 is a const for the best blurring effect
                var receipt = _ocr.ConvertImage(imageNew);
                var itemList = _dataConverter.ReceiptToItemList(receipt);
                return itemList;
            }
            else
            {
                return null;
            }
            
            //_dataManager.DisplayToScreen(itemList);
            //_dataManager.SaveData(itemList);
            
        }

        //temporary method to test image reading. reads all lines, not only items
        public List<string> TempProcessReceiptImage(Bitmap image)
        {
            if (image != null)
            {
                var receipt = _ocr.ConvertImage(image);
                return receipt.LinesOfText;
            }
            else
            {
                return null;
            }
        }
    }
}