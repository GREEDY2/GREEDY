using GREEDY.Models;
using GREEDY.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace GREEDY.UnitTests
{
    [TestClass]
    public class WritingToFileControllerUnitTest
    {
        private const string _GoodFileName = "TestReceipt.txt";
        private const string _BadFileName = "";

 //       private string GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
  //      private string BadFileName = ConfigurationManager.AppSettings["BadFileName"];

        [DataTestMethod]
        [DataRow("1545")]
        [DataRow("     ")]
        [DataRow("")]
        [DataRow("123456 cdcd s")]

        public void WriteTextToGoodFile (string data)
        {
            //arrange
                       Receipt receipt = new Receipt
                      {
                           LinesOfText = new List<String>() { data }
                      };

            //act
            new WritingToFileController().WriteToFile(_GoodFileName, receipt);
            string text = File.ReadAllText(_GoodFileName);

            //assert
            Assert.AreEqual(data + "\r\n", text);
            File.Delete(_GoodFileName);
        }
   
        [DataTestMethod]
        [DataRow("")]
        [DataRow("     ")]
        [DataRow("123456 cdcd s")]

        [ExpectedException(typeof(ArgumentException))]

        public void WriteTextToNotExistFile(string data)
        {
            //arrange
            Receipt receipt = new Receipt
            {
                LinesOfText = new List<String>() { data }
            };

            //act
            new WritingToFileController().WriteToFile("", receipt);

            //assert


        }
    }
}