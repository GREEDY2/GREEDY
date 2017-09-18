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
    class OCRController
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            Receipt receipt = new Receipt();
            var img = new Bitmap("../TestData/Receipt/test1/original.jpg");
            var ocr = new Controllers.OCRController(receipt, img);
            var receiptText = File.ReadAllLines("../TestData/Receipt/test1/originalData.txt").ToList();

            //act
            ocr.UseOCR();

            /*foreach (var line in receipt.LinesOfText)
            {
                textResult.Text += line;
            }*/

            //assert
            Assert.IsTrue(receipt.LinesOfText.IsAlmostEqual(receiptText, receipt.PercentageMatched));
        }
    }
}
