using System;
using System.Windows.Forms;
using GREEDY.DataManagers;
using GREEDY.OCRs;
using GREEDY.Services;
using GREEDY.View;

namespace GREEDY.refactor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            // dependency injection kinda, can search for a framework
            // TODO: normal dependency injection
            Application.Run 
            (
                new MainScreen
                (
                    new ReceiptService 
                    (
                        new EmguOcr (),
                        new DataConverter (), 
                        new DataManager ()
                    )
                )
            );
        }
    }
}
