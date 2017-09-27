using GREEDY.Models;
using GREEDY.Controllers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace GREEDY.IntegrationTest
{
    public class WritingToFileControllerIntegrationTest
    {
        private const string fileName = "TestReceipt.txt";

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("12545dcdd545 dc45sd dc54s")]
        public void WriteTextToFile_StringData_TextInFileAsExpected(string data)
        {
            //arrange
            Receipt receipt = new Receipt
            {
                LinesOfText = new List<String>() { data }
            };
            //act
            new WritingToFileController().WriteToFile(fileName, receipt);
            string text = File.ReadAllText(fileName);
            //assert
            //Assert.AreEqual(data + "\r\n", text);
            Assert.Equal(data + "\r\n", text);
            File.Delete(fileName);
        }
    }
 
}
