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
    public class ExtensionsTests
    {
        /// <summary>
        /// Tests that the correct integrated value is passed back.
        /// </summary>
        [TestMethod()]
        public void ExtractNumberFromFFMPEGLineTest_IntegratedResult()
        {
            var integratedResult = "Input Integrated:     -6.4 LUFS";
            var result = integratedResult.ExtractNumberFromFFMPEGLine();
            Assert.AreEqual("-6.4", result);
        }
        /// <summary>
        /// Tests that the correct True Peak value is passed back.
        /// </summary>
        [TestMethod()]
        public void ExtractNumberFromFFMPEGLineTest_TruePeakResult()
        {
            var truePeakResult = "Input True Peak:      +1.5 dBTP";
            var result = truePeakResult.ExtractNumberFromFFMPEGLine();
            Assert.AreEqual("1.5", result);
        }
        /// <summary>
        /// Tests that the correct LRA value is passed back.
        /// </summary>
        [TestMethod()]
        public void ExtractNumberFromFFMPEGLineTest_LRAResult()
        {
            var lraResult = "Input LRA:             2.1 LU";
            var result = lraResult.ExtractNumberFromFFMPEGLine();
            Assert.AreEqual("2.1", result);
        }
        /// <summary>
        /// Tests that the correct Threshold value is passed back.
        /// </summary>
        [TestMethod()]
        public void ExtractNumberFromFFMPEGLineTest_ThresholdResult()
        {
            var thresholdResult = "Input Threshold:     -16.5 LUFS";
            var result = thresholdResult.ExtractNumberFromFFMPEGLine();
            Assert.AreEqual("-16.5", result);
        }
        /// <summary>
        /// Tests that the correct Offset value is passed back.
        /// </summary>
        [TestMethod()]
        public void ExtractNumberFromFFMPEGLineTest_OffsetResult()
        {
            var offsetResult = "Target Offset:        -0.1 LU";
            var result = offsetResult.ExtractNumberFromFFMPEGLine();
            Assert.AreEqual("-0.1", result);
        }
    }
}