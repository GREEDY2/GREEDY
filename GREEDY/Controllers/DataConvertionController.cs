using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GREEDY.Models;

namespace GREEDY.Controllers
{
    class DataConvertionController
    {
        public void DataTableToXml(DataTable dataTable, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                dataTable.WriteXml(sw);
            }
        }

        public DataTable XmlToDataTable(string path)
        {
            DataSet ds = new DataSet();
            DataTable dt = ds.Tables["ItemPriceList"];
            ds.ReadXml(path);
            return ds.Tables["ItemPriceList"];
        }

        public void ListToXml(List<Item> items, string path)
        {
            XElement xmlElements = new XElement("items", 
                items.Select(i => new XElement("item", 
                new XAttribute("name", i.Name),
                new XAttribute("price", i.Price),
                new XAttribute("category", i.Category))));
            xmlElements.Save(path);
        }

        public List<Item> XmlToList(string path)
        {
            //for reading decimal values from xml
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            var xdoc = XDocument.Load(path);
            var items = from i in xdoc.Descendants("item")
                select new
                {
                    name = (string)i.Attribute("name"),
                    price = decimal.Parse((string)i.Attribute("price")),
                    category = (string)i.Attribute("category")
                };

            List<Item> itemsList = new List<Item>();
            foreach (var item in items)
            {
                itemsList.Add(new Item(item.name, item.price, item.category));
            }

            return itemsList;
        }

        //testinis metodas//o gal net ne testinis. gal sito ir seip reikia
        public DataTable ListToDataTable(List<Item> items)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Price");
            dt.Columns.Add("Category");

            foreach (Item item in items)
            {
                DataRow dr = dt.NewRow();
                dr[0] = item.Name;
                dr[1] = item.Price;
                dr[2] = item.Category;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public List<Item> DataTableToList(DataTable dataTable) //sito gal ir nereikes metodo
        {
            List<Item> items = new List<Item>();

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new Item(row[0].ToString(), decimal.Parse(row[1].ToString()), row[2].ToString()));
            }

            return items;
        }
        
    }
}
