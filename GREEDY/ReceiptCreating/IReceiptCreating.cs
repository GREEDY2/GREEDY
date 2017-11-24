using Geocoding;
using GREEDY.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace GREEDY.ReceiptCreatings
{
    public interface IReceiptCreating
    {
        Receipt FullReceiptCreating(Bitmap image);
        DateTime GetDateForReceipt(List<string> linesOfText);
        Shop GetShopFromData(IEnumerable<string> linesOfText, Location location);
    }
}
