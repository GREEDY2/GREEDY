using GREEDY.Controllers;
using Xunit;
using System.IO;

namespace GREEDY.IntegrationTest
{
    public class CreatePathForDataControllerIntegrationTest
    {
        private const string FilePath = "../../../Data/receipts/TestingPath";
        [Theory]
        [InlineData(FilePath)]
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
        Assert.True(Directory.Exists(data));
        Directory.Delete(data);
        }

        [Theory]
        [InlineData(FilePath)] 
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
        Assert.True(Directory.Exists(data));
        Directory.Delete(data);
        }
    }
}