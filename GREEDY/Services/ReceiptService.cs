using System.Collections.Generic;
using GREEDY.DataManagers;
using GREEDY.Models;
using GREEDY.OCRs;
using System.Drawing;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IImageFormating _imageFormating;
        private readonly IOcr _ocr;
        private readonly IDataConverter _dataConverter;

        public ReceiptService(IImageFormating imageFormating, IOcr ocr, IDataConverter dataConverter)
        {
            _imageFormating = imageFormating;
            _ocr = ocr;
            _dataConverter = dataConverter;
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
        }
    }
}