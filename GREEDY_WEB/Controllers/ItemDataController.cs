using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GREEDY_WEB.Controllers
{
    class Item
    {
        public string Name;
        public decimal Price;
        public string Category;
    }

    [Route("api/[controller]")]
    public class ItemDataController : Controller
    {
        [HttpGet("[action]")]
        public string ItemData()
        {
            var list = new List<Item>();
            list.Add(new Item() { Name = "lol", Price = 2.11m, Category = "lolik" });
            list.Add(new Item() { Name = "blabla", Price = 2m, Category = "l" });
            list.Add(new Item() { Name = "tuscias", Price = 0m, Category = "" });
            return JsonConvert.SerializeObject(list);
        }
    }
}

