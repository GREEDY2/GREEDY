using System;
using System.Collections.Generic;
using GREEDY.DataManagers;
using GREEDY.Models;
using Ploeh.AutoFixture;
using Xunit;

namespace GREEDY.UnitTests.DataManagersUnitTests
{
    public class UnitTestPhotoImageGetter
    {
        [Fact]
        public void ImageGetter_NotImplemented()
        {
            //arrange
            var photoImageGetter = new PhotoImageGetter();
            //act

            //assert
            Assert.Throws(typeof(NotImplementedException), () => photoImageGetter.GetImage());
        }
    }
}
