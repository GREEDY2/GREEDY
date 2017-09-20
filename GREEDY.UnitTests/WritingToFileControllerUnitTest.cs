using GREEDY.Models;
using GREEDY.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GREEDY.UnitTests
{
    [TestClass]
    public class WritingToFileControllerUnitTest
    {
        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("123")]
        public void WriteToFileTest (string data)
        {
            //arrange
            Receipt receipt = new Receipt
            {
                LinesOfText = new List<String>() { data }
            };
            string filePath = "testReceipt.txt";
            //act
            new WritingToFileController().WriteToFile(filePath, receipt);
            string text = System.IO.File.ReadAllText("testReceipt.txt");
            //assert
            Assert.AreEqual(data + "\r\n", text);
            System.IO.File.Delete("testReceipt.txt");
        }
    }
}
