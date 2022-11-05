using Files.Abstractions;
using Microsoft.Extensions.Logging;
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
            // Arrange
            var logger = new Mock<ILogger<FileCreator>>();
            
            var classUnderTest = new FileCreator(logger.Object);
    
            // Act
            var result = classUnderTest.CreateFromStream(new MemoryStream(), "test.txt");
            
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CreateFromStreamTest_Failure()
        {
            // Arrange
            var logger = new Mock<ILogger<FileCreator>>();

            var classUnderTest = new FileCreator(logger.Object);

            // Assert
            Assert.ThrowsException<IOException>(() => classUnderTest.CreateFromStream(new MemoryStream(), "test.txt//"));
        }
    }
}