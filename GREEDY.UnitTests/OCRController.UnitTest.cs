using Microsoft.VisualStudio.TestTools.UnitTesting;
using GREEDY.Models;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //var ocr = new OCRController(receipt, new Bitmap("../TestData/Receipt/test1/original.jpg"));


            //Failed to write a Unit test because of error (I think reference errors)
            //Need to consult with someone who knows how to write Unit Tests.


            //act
            //ocr.UseOCR();

            /*foreach (var line in receipt.LinesOfText)
            {
                textResult.Text += line;
            }*/

            //assert
            //Assert.AreEqual(inside, outside);
        }
    }
}
