using System.Collections.Generic;
using GREEDY.Models;
using System.Drawing;

namespace GREEDY.Services
{
    public interface IReceiptService
    {
        Receipt ProcessReceiptImage(Bitmap image);
    }
}