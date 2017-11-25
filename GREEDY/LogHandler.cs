using System;
using System.Globalization;
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
        private static readonly string logEntrySeperator = new String('-', 60);

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(logEntrySeperator);
                builder.AppendLine("Request");
                builder.AppendLine(DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("en-us")));
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
                        var httpError = response.Content.ReadAsAsync<HttpError>().Result;
                        LogException(builder, httpError);
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

        private void LogException(StringBuilder builder, HttpError httpError)
        {
            builder.AppendLine(httpError.ExceptionType);
            builder.AppendLine(httpError.ExceptionMessage);
            builder.AppendLine(httpError.StackTrace);
            if (httpError.InnerException != null)
            {
                builder.AppendLine("\nInner Exception");
                LogException(builder, httpError.InnerException);
            }
        }
    }
}
