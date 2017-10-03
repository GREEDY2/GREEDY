using System.Collections.Generic;
using GREEDY.Models;
using System.Xml.Linq;
using System.Data;

namespace GREEDY.DataManagers
{
    public interface IDataConverter
    {
        // TODO: remove unnecessary methods
        List<Item> ReceiptToItemList(Receipt receipt);
        XElement ListToXml(List<Item> items);
        DataTable ListToDataTable(List<Item> items);
    }
}