using GREEDY.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GREEDY.IntegrationTest
{
    [TestClass]
    public class CreatePathForDataControllerIntegrationTest
    {
        private const string FilePath = "../../../Data/receipts/TestingPath";
        [TestMethod]
        [Description("Check to see if a folder is created when we create it first time")]
        [DataRow(FilePath)]
        public void CreateFolder_FirstTimeCreating_FolderCreated(string data)
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
        Directory.Delete(data);
        }

        [TestMethod]
        [Description("Check to see if a folder is not replaced when it is exist")]
        [DataRow(FilePath)]
 
        public void CreateFolder_WhereThisFolderExist_FolderDidNotReplaced(string data)
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
    }
}