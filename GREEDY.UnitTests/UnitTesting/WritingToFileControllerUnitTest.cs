using GREEDY.Models;
using GREEDY.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ploeh.AutoFixture;

namespace GREEDY.UnitTests
{
    [TestClass]
    public class WritingToFileControllerUnitTest
    {
        [TestMethod]
        [DataRow("")]
        [DataRow("        ")]
        [ExpectedException(typeof(ArgumentException))]

        public void WriteToFile_IncorectFileName_ArgumentExcention (string filePath)
        {
            //arrange
            var fixture = new Fixture();
            Receipt receipt = fixture.Create<Receipt>();
            
            //act
            new WritingToFileController().WriteToFile(filePath, receipt);
        }

        [TestMethod]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]

        public void WriteToFile_NullFileName_ArgumentNullExcention(string filePath)
        {
            //arrange
            var fixture = new Fixture();
            Receipt receipt = fixture.Create<Receipt>();

            //act
            new WritingToFileController().WriteToFile(filePath, receipt);
        }
    }
}
