using GREEDY.Models;
using GREEDY.ImagePreparation;
using GREEDY.ReceiptCreatings;
using OpenCvSharp;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IImageFormating _imageFormating;
        private readonly IReceiptMaking _receiptCreating;
        private readonly IDataConverter _dataConverter;
        private readonly IItemCategorization _itemCategorization;

        public ReceiptService()
        {
            _imageFormating = new ImageFormating();
            _receiptCreating = new ReceiptMaking();
            _dataConverter = new DataConverter();
            _itemCategorization = new ItemCategorization();
        }

        public ReceiptService(IImageFormating imageFormating, IReceiptMaking receiptCreating, IDataConverter dataConverter, IItemCategorization itemCategorization)
        {
            _imageFormating = imageFormating;
            _receiptCreating = receiptCreating;
            _dataConverter = dataConverter;
            _itemCategorization = itemCategorization;
        }

        public Receipt ProcessReceiptImage(Mat image)
        {
            if (image != null)
            {
                var bitmapImage = _imageFormating.FormatImageForOCR(image);
                var receipt = _receiptCreating.FullReceiptCreating(bitmapImage);
                if (receipt != null)
                {
                    receipt.ItemsList = _dataConverter.ReceiptToItemList(receipt);
                    receipt.ItemsList = _itemCategorization.CategorizeItems(receipt.ItemsList);
                }
                return receipt;
            }
            else
            {
                return null;
            }
        }
    }
}