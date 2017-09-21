using System.IO;

namespace GREEDY.Controllers
{
    public class CreatePathForDataController
    {
        public void CreateAFolder(string filePath)
        {
            Directory.CreateDirectory(filePath);
        }
    }
}
