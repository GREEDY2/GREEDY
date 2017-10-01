using System.Collections.Generic;
using GREEDY.DataManagers;
using GREEDY.Models;
using GREEDY.OCRs;
using System.Drawing;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IOcr _ocr;
        private readonly IDataConverter _dataConverter;
        private readonly IDataManager _dataManager;

        public ReceiptService(IOcr ocr, IDataConverter dataConverter, IDataManager dataManager)
        {
            _ocr = ocr;
            _dataConverter = dataConverter;
            _dataManager = dataManager;
        }

        public List<Item> ProcessReceiptImage(Bitmap image)
        {
            var receipt = _ocr.ConvertImage(image);
            var itemList = _dataConverter.ReceiptToItemList(receipt);
            //_dataManager.DisplayToScreen(itemList);
            //_dataManager.SaveData(itemList);
            return itemList;
        }
    }
}