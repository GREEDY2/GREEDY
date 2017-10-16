using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GREEDY.Models;

namespace GREEDY.Controllers
{
    [Route("api/[controller]")]
    public class ItemDataController : ApiController
    {
        // GET api/values
        public string ItemData()
        {
            var list = new List<Item>();
            list.Add(new Item() { Name = "lol", Price = 2.11m, Category = "lolik" });
            list.Add(new Item() { Name = "blabla", Price = 2m, Category = "l" });
            list.Add(new Item() { Name = "tuscias", Price = 0m, Category = "naxuj" });
            return JsonConvert.SerializeObject(list);
        }
    }
}

