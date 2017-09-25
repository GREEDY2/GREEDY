using GREEDY.Models;
using GREEDY.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace GREEDY.IntegrationTest
{
    [TestClass]
    public class WritingToFileControllerIntegrationTest
    {
        private const string fileName = "TestReceipt.txt";

        [TestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        [DataRow("12545dcdd545 dc45sd dc54s")]
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
            Assert.AreEqual(data + "\r\n", text);
            File.Delete(fileName);
        }
    }
 
}
