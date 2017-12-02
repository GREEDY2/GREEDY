using GREEDY.Models;
using System.Collections.Generic;

namespace GREEDY.ReceiptCreatings
{
    public interface IShopDetection
    {
        Shop GetShopFromData(IEnumerable<string> linesOfText);
    }
}
