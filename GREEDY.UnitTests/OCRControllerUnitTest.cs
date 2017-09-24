using Microsoft.VisualStudio.TestTools.UnitTesting;
using GREEDY.Models;
using System.Linq;
using System.IO;
using GREEDY.Extensions;

namespace GREEDY.UnitTests
{
    [TestClass]
    public class OCRControllerUnitTest
    {


        [TestMethod]
        public void UseOCRTest()
        {
            //arrange
            Receipt receipt = new Receipt();
            var receiptText = File.ReadAllLines("../../TestData/Receipt/test1/originalData.txt").ToList();
            //act
            receipt = new Controllers.OCRController().UseOCR("../../TestData/Receipt/test1/original.jpg");
            //assert
            Assert.IsTrue(receipt.LinesOfText.IsAlmostEqual(receiptText, receipt.PercentageMatched));
            Assert.IsFalse(receipt.LinesOfText.IsAlmostEqual(receiptText, (float)1.01));
            Assert.IsFalse(receipt.LinesOfText.IsAlmostEqual("", (float)0.1));
        }
            
        [TestMethod]
        public void UseOCRTestEmgu()
        {
            //arrange
            Receipt receipt = new Receipt();
            var receiptText = File.ReadAllLines("../../TestData/Receipt/test1/originalData.txt").ToList();
            //act
            receipt = new Controllers.OCRController().UseOCR("../../TestData/Receipt/test1/original.jpg");
            //assert
            Assert.IsTrue(receipt.LinesOfText.IsAlmostEqual(receiptText, (float)0.8));
        }
    }
}