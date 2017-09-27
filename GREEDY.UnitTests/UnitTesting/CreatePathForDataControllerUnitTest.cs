using GREEDY.Controllers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System;

namespace GREEDY.UnitTests
{
    //[TestClass]
    public class CreatePathForDataControllerUnitTest
    {
        [Fact]
        //[Description("Check to see exception for file path (if it Null or Empty).")]

        //[ExpectedException(typeof(ArgumentNullException))]

        public void CreateFolder_WherePathNameIsNullOrEmpty_ExpectedArgumentNullException()
        {
            //arrange
            CreatePathForDataController createPath = new CreatePathForDataController();

            //act
            Assert.Throws(typeof(ArgumentNullException),()=>createPath.CreateAFolder(""));
        }
    }
}