using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
{
    public interface IShopDetection
    {
        Shop GetShopFromData(List<string> linesOfText);
    }
}