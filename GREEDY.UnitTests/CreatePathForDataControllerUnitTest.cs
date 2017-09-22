using GREEDY.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Configuration;

namespace GREEDY.UnitTests
{
    [TestClass]
    public class CreatePathForDataControllerUnitTest
    {

        private const string FilePath = @"C:\Users\Valentinas-PC\Desktop\123";

        [TestMethod]
        [Description("Check to see if a folder is created when we create it first time")]

        [DataRow(FilePath)]

        public void FolderFirstTimeCreating(string data)
        {
            //arrange
            CreatePathForDataController createPath = new CreatePathForDataController();
            DirectoryInfo folderDirectory = new DirectoryInfo(data);

            //act
            if (Directory.Exists(data))
            {
                foreach (FileInfo file in folderDirectory.GetFiles())
                {
                    file.Delete();
                }

                Directory.Delete(data);
            }

            createPath.CreateAFolder(data);

            //assert
            Assert.IsTrue(Directory.Exists(data));
        //    Directory.Delete(data);
        }

        [TestMethod]
        [Description("Check to see if a folder is not replaced when it is exist")]
        [DataRow(FilePath)]

        public void FolderNotFirstTimeCreating(string data)
        {
            //arrange
            CreatePathForDataController createPath = new CreatePathForDataController();
            DirectoryInfo folderDirectory = new DirectoryInfo(data);

            //act
            if (!Directory.Exists(data))
            {
                createPath.CreateAFolder(data);
            }
            createPath.CreateAFolder(data);

            //assert
            Assert.IsTrue(Directory.Exists(data));
            Directory.Delete(data);
        }

        [TestMethod]
        [Description("Check to see exception for file path (if it Null or Empty).")]

        [ExpectedException(typeof (ArgumentNullException))]

        public void PathNameIsNullOrEmpty()
        {
            //arrange
            CreatePathForDataController createPath = new CreatePathForDataController();

            //act
            createPath.CreateAFolder("");
            //assert
        }
    }
}