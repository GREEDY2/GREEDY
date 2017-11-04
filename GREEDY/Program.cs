using System;
using Microsoft.Owin.Hosting;

namespace GREEDY
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string domainAddress = Environments.AppConfig.ServerAdressAndPort;
            using (WebApp.Start<Startup>(url: domainAddress))
            {
                Console.WriteLine("Service Hosted " + domainAddress);
                System.Threading.Thread.Sleep(-1);
            }
        }
    }
}
