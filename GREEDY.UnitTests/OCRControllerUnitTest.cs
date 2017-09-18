using Microsoft.VisualStudio.TestTools.UnitTesting;
using GREEDY.Models;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var img = new Bitmap("../../TestData/Receipt/test1/original.jpg");
            var ocr = new Controllers.OCRController(receipt, img);
            var receiptText = File.ReadAllLines("../../TestData/Receipt/test1/originalData.txt").ToList();

            //act
            ocr.UseOCR();

            //assert
            Assert.IsTrue(receipt.LinesOfText.IsAlmostEqual(receiptText, receipt.PercentageMatched));
            Assert.IsFalse(receipt.LinesOfText.IsAlmostEqual(receiptText, (float)1.01));
            Assert.IsFalse(receipt.LinesOfText.IsAlmostEqual("", (float)0.1));
        }
    }
}
