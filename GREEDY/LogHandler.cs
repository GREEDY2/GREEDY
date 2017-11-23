using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace GREEDY
{
    public class LogHandler : DelegatingHandler
    {
        private static object lockObject = new object();
        private string logEntrySeperator => "---------------------------------------------------";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response=null;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(logEntrySeperator);
                builder.AppendLine("Request");
                builder.AppendLine(DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss"));
                builder.AppendLine(string.Format("Request URI: {0}", request.RequestUri.ToString()));
                builder.AppendLine(string.Format("Method: {0}", request.Method));

                response = await base.SendAsync(request, cancellationToken);
                builder.AppendLine("\nResponse");
                builder.AppendLine(string.Format("StatusCode: {0}", response.StatusCode.ToString()));
                if (response.Content != null)
                {
                    if ((int)response.StatusCode >= 400)
                    {
                        string contentString = response.Content.ReadAsStringAsync().Result;
                        var httpError = response.Content.ReadAsAsync<HttpError>();
                        var errorValues = httpError.Result.Values.Select(x => x).ToList();
                        foreach (string s in errorValues)
                            builder.AppendLine(s);
                    }
                    else
                    {
                        builder.AppendLine(string.Format("Content: {0}", response.Content.ReadAsStringAsync().Result));
                        builder.AppendLine(string.Format("MediaType:{0}", response.Content.Headers.ContentType.MediaType));
                    }
                }
                builder.AppendLine(logEntrySeperator);
                Task.Run(() => WriteLogEntry(builder.ToString()));
                return response;
            }
            catch
            {
                return response;
            }
        }

        private void WriteLogEntry(string logEntry)
        {
            lock (lockObject)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(Environments.AppConfig.LogPath, true))
                {
                    file.Write(logEntry);
                }
            }
        }
    }
}
