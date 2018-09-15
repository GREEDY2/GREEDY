using System;
using System.Collections.Generic;
using System.Drawing;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
{
    public interface IReceiptMaking
    {
        Receipt FullReceiptCreating(Bitmap image);
        DateTime? GetDateForReceipt(List<string> linesOfText);
    }
}