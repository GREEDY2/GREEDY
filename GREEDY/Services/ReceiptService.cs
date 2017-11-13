using System.Collections.Generic;
using GREEDY.DataManagers;
using GREEDY.Models;
using System.Drawing;
using System;
using GREEDY.ReceiptCreating;

namespace GREEDY.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IImageFormating _imageFormating;
        private readonly IReceiptCreating _receiptCreating;
        private readonly IDataConverter _dataConverter;
        private static AutoCorrect autoCorrect => new AutoCorrect();

        public ReceiptService()
        {
            _imageFormating = new ImageFormating();
            _receiptCreating = new ReceiptCreating();
            _dataConverter = new DataConverter();
        }

        public ReceiptService(IImageFormating imageFormating, IReceiptCreating receiptCreating, IDataConverter dataConverter)
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
                receipt.ItemsList = AutoCorrect(receipt.ItemsList);
                return receipt;
            }
            else
            {
                return null;
            }
        }
    }
}