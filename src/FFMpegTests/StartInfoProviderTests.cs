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
    public class StartInfoProviderTests
    {
        [TestMethod()]
        public void GetStartInfoTest()
        {
            //Arrange
            var testStartInfoProvider = new StartInfoProvider();
            
            //Act
            var startInfo = testStartInfoProvider.GetStartInfo("args -test");

            //Asser
            Assert.AreEqual(true, startInfo.CreateNoWindow);
            Assert.AreEqual(false, startInfo.UseShellExecute);
            Assert.AreEqual(true, startInfo.RedirectStandardOutput);
            Assert.AreEqual(true, startInfo.RedirectStandardError);
        }
    }
}