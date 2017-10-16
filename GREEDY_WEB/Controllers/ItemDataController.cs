using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GREEDY.Controllers
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
            list.Add(new Item() { Name = "krc", Price = 2.11m, Category = "dar" });
            list.Add(new Item() { Name = "bsk", Price = 2m, Category = "neveik" });
            list.Add(new Item() { Name = ":(((", Price = 0m, Category = "sad" });
            return JsonConvert.SerializeObject(list);
        }
    }
}

