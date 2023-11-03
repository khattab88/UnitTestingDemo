﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky.Tests.XUnit
{
    public class BankAccountTests
    {
        private BankAccount _account;

        public BankAccountTests()
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

        [Fact]
        public void Deposit_Add100_ReturnsTrue()
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(x => x.Log("Deposit invoked!"));

            BankAccount account = new(mockLogger.Object);

            bool result = account.Deposit(100);

            Assert.True(result);
            Assert.Equal(100, account.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void Withdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int amount)
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.LogToDb(It.IsAny<string>())).Returns(true);
            mockLogger.Setup(m => m.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount account = new(mockLogger.Object);
            account.Deposit(balance);

            bool result = account.Withdraw(amount);

            Assert.True(result);
        }

        [Theory]
        [InlineData(200, 300)]
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

            Assert.False(result);
        }
    }
}