using System;
using System.Threading;
using Microsoft.Owin.Hosting;

namespace GREEDY
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var domainAddress = Environments.AppConfig.ServerAdressAndPort;
            using (WebApp.Start<Startup>(domainAddress))
            {
                Console.WriteLine("Service Hosted " + domainAddress);
                Thread.Sleep(-1);
            }
        }
    }
}