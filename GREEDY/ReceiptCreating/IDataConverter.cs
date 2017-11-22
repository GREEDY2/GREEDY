using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.ReceiptCreatings
{
    public interface IDataConverter
    {
        List<Item> ReceiptToItemList(Receipt receipt);
        List<Item> CategorizeItems(List<Item> itemsList);
    }
}