using System;
using System.Windows.Forms;
using GREEDY.DataManagers;
using GREEDY.OCRs;
using GREEDY.Services;
using GREEDY.View;
using System.Net;
using System.Threading;
using System.Text;
using Microsoft.Owin.Hosting;

namespace GREEDY
{
    static class Program
    {
        static HttpListener _httpListener = new HttpListener();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string domainAddress = "http://localhost:6967/";
            using (WebApp.Start(url: domainAddress))
            {
                Console.WriteLine("Service Hosted " + domainAddress);
                System.Threading.Thread.Sleep(-1);
            }
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run
            (
                new MainScreen
                (
                    new ReceiptService
                    (
                        new EmguOcr(),
                        new DataConverter(),
                        new DataManager()
                    ),
                    new ItemService
                    (
                        new DataConverter(),
                        new DataManager(),
                        new ItemCategorization()
                    )
                )
            );*/
        }
    }
}
