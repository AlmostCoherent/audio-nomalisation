//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using FFMpeg;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Moq;
//using Microsoft.Extensions.Logging;
//using System.Reflection;

//namespace FFMpegTests
//{
//    [TestClass()]
//    public class LUFSProviderTests
//    {
//        public LUFSProviderTests()
//        {
//        }

//        [TestMethod()]
//        public void GetMeLUFSTest()
//        {
//            //Arrange
//            var logger = new Mock<ILogger>();
//            var gmlfs = new LUFSProviderV2();
//            var inputFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TestAudio\\XF_HatAmp001.wav";
//            //Act
//            gmlfs.GetMeLUFS(inputFile);

//            //Assert
////            Assert.Fail();
//        }
//    }
//}