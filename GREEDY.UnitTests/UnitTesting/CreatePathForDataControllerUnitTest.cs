using GREEDY.Controllers;
using Xunit;
using System;

namespace GREEDY.UnitTests
{
    public class CreatePathForDataControllerUnitTest
    {
        [Fact]
        public void CreateFolder_WherePathNameIsNullOrEmpty_ExpectedArgumentNullException()
        {
            //arrange
            CreatePathForDataController createPath = new CreatePathForDataController();

            //act
            Assert.Throws(typeof(ArgumentNullException),()=>createPath.CreateAFolder(""));
        }
    }
}