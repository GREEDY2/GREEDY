using GREEDY.Models;
using System.Drawing;
using GREEDY.ImagePreparation;
using GREEDY.ReceiptCreatings;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IImageFormating _imageFormating;
        private readonly IReceiptCreatings _receiptCreating;
        private readonly IDataConverter _dataConverter;

        public ReceiptService()
        {
            _imageFormating = new ImageFormating();
            _receiptCreating = new ReceiptCreating();
            _dataConverter = new DataConverter();
        }

        public ReceiptService(IImageFormating imageFormating, IReceiptCreatings receiptCreating, IDataConverter dataConverter)
        {
            _imageFormating = imageFormating;
            _receiptCreating = receiptCreating;
            _dataConverter = dataConverter;
        }

        public Receipt ProcessReceiptImage(Bitmap image)
        {
            if (image != null)
            {
                image = _imageFormating.FormatImage(image);
                var receipt = _receiptCreating.FullReceiptCreating(image);
                receipt.ItemsList = _dataConverter.ReceiptToItemList(receipt);
                receipt.ItemsList = _dataConverter.CategorizeItems(receipt.ItemsList);
                return receipt;
            }
            else
            {
                return null;
            }
        }
    }
}