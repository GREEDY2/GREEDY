using GREEDY.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    class WritingToFileController
    {
        private Receipt Receipt;
        private string FilePath;

        public WritingToFileController (Receipt model, string filePath)
        {
            this.Receipt = model;
            this.FilePath = filePath;
        }

        public void WriteToFile ()
        {
            using (TextWriter tw = new StreamWriter(FilePath))
            {
                foreach (String line in Receipt.LinesOfText)
                {
                    tw.WriteLine(line);
                }
            }
        }
    }
}
