using System.Collections.Generic;
using GREEDY.Models;
using System.Xml.Linq;
using System.Data;

namespace GREEDY.DataManagers
{
    public interface IDataConverter
    {
        List<Item> ReceiptToItemList(Receipt receipt);
        XElement ListToXml(List<Item> items);
        DataTable ListToDataTable(List<Item> items);
    }
}