using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace GREEDY
{
    public class LogHandler : DelegatingHandler
    {
        private static readonly object LockObject = new object();
        private static readonly string LogEntrySeperator = new string('-', 60);

        [SuppressMessage("Await.Warning", "CS4014:Await.Warning")]
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            try
            {
                var builder = new StringBuilder();
                builder.AppendLine(LogEntrySeperator);
                builder.AppendLine("Request");
                builder.AppendLine(DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("en-us")));
                builder.AppendLine($"Request URI: {request.RequestUri}");
                builder.AppendLine($"Method: {request.Method}");

                response = await base.SendAsync(request, cancellationToken);
                builder.AppendLine("\nResponse");
                builder.AppendLine($"StatusCode: {response.StatusCode.ToString()}");
                if (response.Content != null)
                {
                    if ((int) response.StatusCode >= 400)
                    {
                        var contentString = response.Content.ReadAsStringAsync().Result;
                        var httpError = response.Content.ReadAsAsync<HttpError>(cancellationToken).Result;
                        LogException(builder, httpError);
                    }
                    else
                    {
                        builder.AppendLine($"Content: {response.Content.ReadAsStringAsync().Result}");
                        builder.AppendLine($"MediaType:{response.Content.Headers.ContentType.MediaType}");
                    }
                }

                builder.AppendLine(LogEntrySeperator);
#pragma warning disable 4014
                Task.Run(() => WriteLogEntry(builder.ToString()), cancellationToken);
#pragma warning restore 4014
                return response;
            }
            catch
            {
                return response;
            }
        }

        private static void WriteLogEntry(string logEntry)
        {
            lock (LockObject)
            {
                using (var file = new StreamWriter(Environments.AppConfig.LogPath, true))
                {
                    file.Write(logEntry);
                }
            }
        }

        private static void LogException(StringBuilder builder, HttpError httpError)
        {
            while (true)
            {
                builder.AppendLine(httpError.ExceptionType);
                builder.AppendLine(httpError.ExceptionMessage);
                builder.AppendLine(httpError.StackTrace);
                if (httpError.InnerException == null) return;
                builder.AppendLine("\nInner Exception");
                httpError = httpError.InnerException;
            }
        }
    }
}