using AudioFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioFile.Tests
{
    [TestClass()]
    public class FileSystemProviderTests
    {
        [TestMethod()]
        public void GetAudioFileTest()
        {
            var audioFileProvider = new FileSystemProvider();
            var audio  = audioFileProvider.GetAudioFile(@"D:\OneDrive\Music\SubCat Tracks\Devilair x SubCat EP\SubCat - Hyenas [MST].wav");
            Assert.IsNotNull(audio);
        }
    }
}