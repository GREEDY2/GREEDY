using System;
using System.Windows.Forms;
using GREEDY.DataManagers;
using GREEDY.OCRs;
using GREEDY.Services;
using GREEDY.View;

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
            Application.EnableVisualStyles();
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
            );
        }
    }
}
