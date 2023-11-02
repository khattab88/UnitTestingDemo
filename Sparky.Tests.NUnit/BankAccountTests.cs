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
        public void Deposit_CreateAccountWithFakeLoggerAdd100_ReturnsTrue()
        {
            BankAccount account = new(new FakeLogger());

            bool result = account.Deposit(100);

            Assert.IsTrue(result);
            Assert.That(account.GetBalance(), Is.EqualTo(100));
        }
    }
}