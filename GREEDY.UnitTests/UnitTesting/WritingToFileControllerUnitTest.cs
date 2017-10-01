using GREEDY.Models;
using GREEDY.Controllers;
using System;
using Ploeh.AutoFixture;
using Xunit;

namespace GREEDY.UnitTests
{

    public class WritingToFileControllerUnitTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        public void WriteToFile_IncorectFileName_ArgumentExcention (string filePath)
        {
            //arrange
            var fixture = new Fixture();
            Receipt receipt = fixture.Create<Receipt>();
            
            //act
            Assert.Throws(typeof(ArgumentException),()=>new WritingToFileController().WriteToFile(filePath, receipt));
        }

        [Theory]
        [InlineData(null)]
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
