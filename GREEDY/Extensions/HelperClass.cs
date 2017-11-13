using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;

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

        public static List<T> TakeEveryNth<T>(this List<T> source, int n)
        {
            List<T> newCollection = new List<T>();

            for (int i = 0; i < source.Count; i += n)
            {
                newCollection.Add(source[i]);
            }

            return newCollection;
        }

    }
}
