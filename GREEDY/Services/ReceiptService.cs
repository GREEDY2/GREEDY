using GREEDY.Models;
using System.Drawing;
using GREEDY.ImagePreparation;
using GREEDY.ReceiptCreatings;

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

        public Receipt ProcessReceiptImage(Bitmap image)
        {
            if (image != null)
            {
                image = _imageFormating.FormatImage(image);
                var receipt = _receiptCreating.FullReceiptCreating(image);
                receipt.ItemsList = _dataConverter.ReceiptToItemList(receipt);
                receipt.ItemsList = _itemCategorization.CategorizeItems(receipt.ItemsList);
                return receipt;
            }
            else
            {
                return null;
            }
        }
    }
}