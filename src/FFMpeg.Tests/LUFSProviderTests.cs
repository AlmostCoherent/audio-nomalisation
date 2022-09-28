using Microsoft.VisualStudio.TestTools.UnitTesting;
using FFMpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMpeg.Tests
{
    [TestClass()]
    public class LUFSProviderTests
    {
        [TestMethod()]
        public void LUFSProviderTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LUFSProvider(default));
        }
    }
}