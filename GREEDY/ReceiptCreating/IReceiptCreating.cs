using GREEDY.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GREEDY.ReceiptCreatings
{
    public interface IReceiptCreatings
    {
        Receipt FullReceiptCreating(Bitmap image);
        DateTime GetDateForReceipt(List<string> linesOfText);
    }
}
