using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.NUnit
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer _customer;

        [SetUp]
        public void Setup()
        {
            _customer = new Customer();
        }

        [Test]
        public void Greet_InputFirstAndLastNames_ReturnsFullNameGreeting()
        {
            var result = _customer.Greet("John", "Doe");

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo("Hello, John Doe."));
                Assert.That(result, Does.Contain(","));
                Assert.That(result, Does.Contain("john doe").IgnoreCase);
                Assert.That(result, Does.StartWith("Hello"));
                Assert.That(result, Does.EndWith("."));
                Assert.That(result, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+."));
            });
        }

        [Test]
        public void Greeting_GreetNotCalled_ReturnsNull()
        {
            Assert.IsNull(_customer.Greeting);
        }

        [Test]
        public void Greeting_GreetCalled_ReturnsGreeting()
        {
            _customer.Greet("John", "Doe");
            Assert.IsNotNull(_customer.Greeting);
        }

        [Test]
        public void Greeting_GreetWithoutLastName_ReturnsNotNull()
        {
            _customer.Greet("Ben", "");

            Assert.IsNotNull(_customer.Greeting);
            Assert.IsFalse(string.IsNullOrEmpty(_customer.Greeting));
        }

        [Test]
        public void Greet_EmptyFirstName_ThrowsArgumentException()
        {
            //var exceptionDetails = Assert.Throws<ArgumentException>(
            //    () => _customer.Greet("", "Stark"));

            // Assert.AreEqual("First name cannot be null!", exceptionDetails?.Message);

            Assert.Throws<ArgumentException>(() => _customer.Greet("", "Stark"));

            Assert.That(() => { _customer.Greet("", "Stark"); }, 
                Throws.ArgumentException.With.Message.EqualTo("First name cannot be null!"));
        }

        [Test]
        public void CheckDiscount_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = _customer.Discount;
            Assert.That(result, Is.InRange(10,25));
        }

        [Test]
        public void GetCustomerDetails_CreateCustomerWithOrderTotalLessThan100_ReturnsBasicCustomer()
        {
            _customer.OrderTotal = 10;
            CustomerType result = _customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void GetCustomerDetails_CreateCustomerWithOrderTotalMoreThan100_ReturnsPremiumCustomer()
        {
            _customer.OrderTotal = 200;
            CustomerType result = _customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<PremiumCustomer>());
        }
    }
}
