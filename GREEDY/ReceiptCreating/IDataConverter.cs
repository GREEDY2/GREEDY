using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.ReceiptCreating
{
    public interface IDataConverter
    {
        List<Item> ReceiptToItemList(Receipt receipt);
    }
}