using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GREEDY.Controllers
{
    class DataTableXmlController
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
        
    }
}
