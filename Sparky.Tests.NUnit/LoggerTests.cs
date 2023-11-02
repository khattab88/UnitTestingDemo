using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.NUnit
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Log_WithLogWithReturnString_ReturnsTrue()
        {
            string expected = "hello";

            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogWithReturnString(It.IsAny<string>()))
                .Returns((string str) => str.ToLower());

            Assert.That(mockLogger.Object.LogWithReturnString("Hello"), Is.EqualTo(expected));
        }

        [Test]
        public void Log_WithLogReturnsOutputResult_ReturnsTrue()
        {
            var logMock = new Mock<ILogger>();
            string desiredOutput = "hello";

            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(desiredOutput));
        }

        [Test]
        public void Log_GetAndSetLogProperties()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogSeverity).Returns(10);
            mockLogger.Setup(m => m.LogType).Returns("INFO");

            Assert.That(mockLogger.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(mockLogger.Object.LogType, Is.EqualTo("INFO"));
        }

        [Test]
        public void Log_MockingCallbacks()
        {
            string logTemp = "Hello, ";

            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogToDb(It.IsAny<string>()))
                .Returns(true)
                .Callback((string str) => logTemp += str);

            mockLogger.Object.LogToDb("Ben");

            Assert.That(logTemp, Is.EqualTo("Hello, Ben"));
        }
    }
}
