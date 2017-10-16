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
        private readonly IOcr _ocr;
        private readonly IDataConverter _dataConverter;
        private readonly IDataManager _dataManager;

        public ReceiptService()
        {
            _ocr = new EmguOcr();
            _dataConverter = new DataConverter();
            _dataManager = new DataManager();
        }

        public ReceiptService(IOcr ocr, IDataConverter dataConverter, IDataManager dataManager)
        {
            _ocr = ocr;
            _dataConverter = dataConverter;
            _dataManager = dataManager;
        }

        public List<Item> ProcessReceiptImage(Bitmap image)
        {
            if (image != null)
            {
                var receipt = _ocr.ConvertImage(image);
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
    }
}