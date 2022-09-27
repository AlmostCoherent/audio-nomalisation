using Microsoft.VisualStudio.TestTools.UnitTesting;
using FFMpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMpegTests
{
    [TestClass()]
    public class ResultBuilderTests
    {
        [TestMethod()]
        public void ResultBuilderTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ResultBuilder(default, default));
            Assert.ThrowsException<ArgumentNullException>(() => new ResultBuilder("c:\\test", default));
        }
    }
}