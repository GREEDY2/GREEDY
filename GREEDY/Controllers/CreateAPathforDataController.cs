using GREEDY.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.Controllers
{
    class CreateAPathforDataController
    {
        private string FilePath;

        public CreatePathForDataController(string FilePath)
        {
            this.FilePath = FilePath;
        }

        public void CreateAFolder()
        {
            System.IO.Directory.CreateDirectory(FilePath);
        }
    }
}
