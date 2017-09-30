using System.Collections.Generic;
using GREEDY.Models;

namespace GREEDY.DataManagers
{
    public interface IDataConverter
    {
        List<Item> ReceiptToItemList (Receipt receipt);
    }
}