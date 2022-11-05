using Microsoft.VisualStudio.TestTools.UnitTesting;
using FFMpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpeg.Abstractions;
using Moq;

namespace FFMpeg.Tests
{
    [TestClass()]
    public class ExecuteProcessTests
    {
        [TestMethod()]
        public void ExecuteProcessNullConstructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ExecuteProcess(default, default));
            Assert.ThrowsException<ArgumentNullException>(() => new ExecuteProcess(Mock.Of<IStartInfoProvider>(), default));
        }
    }
}