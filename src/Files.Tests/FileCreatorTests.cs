using Files.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Files.Tests
{
    [TestClass()]
    public class FileCreatorTests
    {
        [TestMethod()]
        public void CreateFromStreamTest()
        {
            var mock = new Mock<IBaseFileConfig>();
            mock.Setup(m => m.OutputPath)
                .Returns(Directory.GetParent(Environment.CurrentDirectory).ToString());
            
            var classUnderTest = new FileCreator(mock.Object);
            var result = classUnderTest.CreateFromStream(new MemoryStream(), "test.txt");
            Assert.Fail();
        }
    }
}