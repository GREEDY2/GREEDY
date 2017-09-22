using System.IO;
using System;

namespace GREEDY.Controllers
{
    public class CreatePathForDataController
    {
        public void CreateAFolder(string filePath)
        {
            if (Directory.Exists(filePath))
            {
                return;
            }
            else if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            else
            {
                Directory.CreateDirectory(filePath);
            }
        }
    }
}
