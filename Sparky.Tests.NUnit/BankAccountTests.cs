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
            _account = new (new Logger());
        }

        [Test]
        public void Deposit_Add100_ReturnsTrue() 
        {
            bool result = _account.Deposit(100);

            Assert.IsTrue(result);
            Assert.That(_account.GetBalance(), Is.EqualTo(100));
        }
    }
}