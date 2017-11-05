using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GREEDY.Extensions
{
    public static class HelperClass
    {
        public static HttpResponseMessage JsonHttpResponse<T>(T objectToSendAsJson)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                JsonConvert.SerializeObject(objectToSendAsJson),
                Encoding.UTF8,
                "text/html"
                )
            };
        }
        
    }
}
