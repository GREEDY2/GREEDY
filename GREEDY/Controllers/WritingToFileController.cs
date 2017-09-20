using GREEDY.Models;
using System;
using System.IO;

namespace GREEDY.Controllers
{
    class WritingToFileController
    {
        public void WriteToFile (string filePath, Receipt receipt)
        {
            using (TextWriter tw = new StreamWriter(filePath))
            {
                foreach (String line in receipt.LinesOfText)
                {
                    tw.WriteLine(line);
                }
            }
        }
    }
}
