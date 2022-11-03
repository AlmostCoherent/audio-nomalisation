using Microsoft.VisualStudio.TestTools.UnitTesting;
using Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;

namespace Files.Tests
{
    [TestClass()]
    public class FileRemoverTests
    {
        [TestMethod()]
        public void RemoveByPathTest()
        {
            // Arrange
            string fileName = "c:\\Temp\\UnitTest.txt";
            FileInfo fileInfo = new FileInfo(fileName);
            var logger = new Mock<ILogger<FileCreator>>();

            FileStream? fileStream = default;
            try
            {
                fileStream = File.Create(Path.Combine(fileName));
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Dispose();
            }

            var fileRemover = new FileRemover(logger.Object);

            // Act
            bool success = fileRemover.RemoveByPath(fileInfo.FullName);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void RemoveByPathTest_Failure_CantFindFile()
        {
            // Arrange
            string fileName = "c:\\Temp\\UnitTest.txt";
            FileInfo fileInfo = new FileInfo(fileName);
            var logger = new Mock<ILogger<FileCreator>>();

            var fileRemover = new FileRemover(logger.Object);

            // Act
            bool success = fileRemover.RemoveByPath(fileInfo.FullName);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod()]
        public void RemoveByPathTest_Failure_IOException()
        {
            // Arrange
            string fileName = "c:\\Temp\\UnitTest.txt";
            FileInfo fileInfo = new FileInfo(fileName);
            var logger = new Mock<ILogger<FileCreator>>();


            FileStream? fileStream = default;
            fileStream = File.Create(Path.Combine(fileName));

            var fileRemover = new FileRemover(logger.Object);

            // Assert
            Assert.ThrowsException<IOException>(() => fileRemover.RemoveByPath(fileInfo.FullName));

            if (fileStream != null)
                fileStream.Dispose();

            fileRemover.RemoveByPath(fileInfo.FullName);
        }
    }
}