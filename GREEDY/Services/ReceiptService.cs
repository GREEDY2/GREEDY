using GREEDY.ImagePreparation;
using GREEDY.Models;
using GREEDY.ReceiptCreating;
using OpenCvSharp;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IDataConverter _dataConverter;
        private readonly IImageFormating _imageFormating;
        private readonly IItemCategorization _itemCategorization;
        private readonly IReceiptMaking _receiptCreating;

        public ReceiptService(IImageFormating imageFormating, IReceiptMaking receiptCreating,
            IDataConverter dataConverter, IItemCategorization itemCategorization)
        {
            _imageFormating = imageFormating;
            _receiptCreating = receiptCreating;
            _dataConverter = dataConverter;
            _itemCategorization = itemCategorization;
        }

        public Receipt ProcessReceiptImage(Mat image)
        {
            if (image == null) return null;
            var bitmapImage = _imageFormating.FormatImageForOcr(image);
            var receipt = _receiptCreating.FullReceiptCreating(bitmapImage);
            if (receipt == null) return null;
            receipt.ItemsList = _dataConverter.ReceiptToItemList(receipt);
            receipt.ItemsList = _itemCategorization.CategorizeItems(receipt.ItemsList);

            return receipt;
        }
    }
}