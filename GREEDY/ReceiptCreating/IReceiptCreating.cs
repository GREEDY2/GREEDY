using GREEDY.Models;
using System.Collections.Generic;
using System.Drawing;

namespace GREEDY.DataManagers
{
    public interface IReceiptCreating
    {
        Receipt FullReceiptCreating(Bitmap image);
        string GetDateForReceipt(List<string> linesOfText);
        Shop GetShopFromData(List<string> linesOfText);
    }
}
