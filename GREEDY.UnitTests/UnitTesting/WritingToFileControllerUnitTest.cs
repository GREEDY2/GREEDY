using GREEDY.Models;
using GREEDY.Controllers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ploeh.AutoFixture;
using Xunit;

namespace GREEDY.UnitTests
{
    //[TestClass]
    public class WritingToFileControllerUnitTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        //[ExpectedException(typeof(ArgumentException))]

        public void WriteToFile_IncorectFileName_ArgumentExcention (string filePath)
        {
            throw(new Exception());
            //arrange
            var fixture = new Fixture();
            Receipt receipt = fixture.Create<Receipt>();
            
            //act
            Assert.Throws(typeof(ArgumentException),()=>new WritingToFileController().WriteToFile(filePath, receipt));
        }

        [Theory]
        [InlineData(null)]
        //[ExpectedException(typeof(ArgumentNullException))]

        public void WriteToFile_NullFileName_ArgumentNullExcention(string filePath)
        {
            //arrange
            var fixture = new Fixture();
            Receipt receipt = fixture.Create<Receipt>();

            //act
            Assert.Throws(typeof(ArgumentNullException),()=>new WritingToFileController().WriteToFile(filePath, receipt));
        }
    }
}
