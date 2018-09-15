using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace GREEDY.Extensions
{
    public static class HelperClass
    {
        public static HttpResponseMessage JsonHttpResponse<T>(T objectToSendAsJson)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(objectToSendAsJson),
                    Encoding.UTF8,
                    "text/html"
                )
            };
        }

        public static List<T> TakeEveryNth<T>(this List<T> source, int n)
        {
            var newCollection = new List<T>();

            for (var i = 0; i < source.Count; i += n) newCollection.Add(source[i]);

            return newCollection;
        }
    }
}