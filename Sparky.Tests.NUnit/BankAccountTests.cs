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
    public class BankAccountTests
    {
        private BankAccount _account;

        [SetUp]
        public void SetUp()
        {
        }

        //[Test]
        //public void Deposit_CreateAccountWithFakeLoggerAdd100_ReturnsTrue()
        //{
        //    BankAccount account = new(new FakeLogger());

        //    bool result = account.Deposit(100);

        //    Assert.IsTrue(result);
        //    Assert.That(account.GetBalance(), Is.EqualTo(100));
        //}

        [Test]
        public void Deposit_Add100_ReturnsTrue()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.Log("Deposit invoked!"));

            BankAccount account = new(mockLogger.Object);

            bool result = account.Deposit(100);

            Assert.IsTrue(result);
            Assert.That(account.GetBalance(), Is.EqualTo(100));
        }

        [Test]
        [TestCase(200, 100)]
        [TestCase(200, 150)]
        public void Withdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int amount)
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogToDb(It.IsAny<string>())).Returns(true);
            mockLogger.Setup(m => m.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount account = new(mockLogger.Object);
            account.Deposit(balance);

            bool result = account.Withdraw(amount);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(200, 300)]
        public void Withdraw_Withdraw300With200Balance_ReturnsFalse(int balance, int amount)
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogToDb(It.IsAny<string>())).Returns(true);
            mockLogger.Setup(m => m.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            // mockLogger.Setup(m => m.LogBalanceAfterWithdrawal(It.Is<int>(x => x < 0))).Returns(false);
            // mockLogger.Setup(m => m.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount account = new(mockLogger.Object);
            account.Deposit(balance);

            bool result = account.Withdraw(amount);

            Assert.IsFalse(result);
        }

        [Test]
        public void DummyLog_WithLogWithReturnString_ReturnsTrue()
        {
            string expected = "hello";

            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogWithReturnString(It.IsAny<string>()))
                .Returns((string str) => str.ToLower());

            Assert.That(mockLogger.Object.LogWithReturnString("Hello"), Is.EqualTo(expected));
        }

        [Test]
        public void DummyLog_WithLogReturnsOutputResult_ReturnsTrue()
        {
            var logMock = new Mock<ILogger>();
            string desiredOutput = "hello";

            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
            Assert.That(result, Is.EqualTo(desiredOutput));
        }
    }
}