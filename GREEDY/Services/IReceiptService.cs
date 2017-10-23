using System.Collections.Generic;
using GREEDY.Models;
using System.Drawing;

namespace GREEDY.Services
{
    public interface IReceiptService
    {
        List<Item> ProcessReceiptImage(Bitmap image);
    }
}