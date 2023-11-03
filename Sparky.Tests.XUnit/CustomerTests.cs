using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky.Tests.XUnit
{
    public class CustomerTests
    {
        private Customer _customer;

        public CustomerTests()
        {
            _customer = new Customer();
        }

        [Fact]
        public void Greet_InputFirstAndLastNames_ReturnsFullNameGreeting()
        {
            var result = _customer.Greet("John", "Doe");

            Assert.Equal("Hello, John Doe.", result);
            Assert.Contains(",", result);
            Assert.Contains("John Doe", result);
            Assert.StartsWith("Hello", result);
            Assert.EndsWith(".", result);
            Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+.", result);
        }

        [Fact]
        public void Greeting_GreetNotCalled_ReturnsNull()
        {
            Assert.Null(_customer.Greeting);
        }

        [Fact]
        public void Greeting_GreetCalled_ReturnsGreeting()
        {
            _customer.Greet("John", "Doe");
            Assert.NotNull(_customer.Greeting);
        }

        [Fact]
        public void Greeting_GreetWithoutLastName_ReturnsNotNull()
        {
            _customer.Greet("Ben", "");

            Assert.NotNull(_customer.Greeting);
            Assert.False(string.IsNullOrEmpty(_customer.Greeting));
        }

        [Fact]
        public void Greet_EmptyFirstName_ThrowsArgumentException()
        {
            var exceptionDetails = Assert.Throws<ArgumentException>(() => _customer.Greet("", "Spark"));
            Assert.Equal("First name cannot be null!", exceptionDetails.Message);

            Assert.Throws<ArgumentException>(() => _customer.Greet("", "Spark"));
        }

        [Fact]
        public void CheckDiscount_DefaultCustomer_ReturnsDiscountInRange()
        {
            int result = _customer.Discount;
            Assert.InRange(result, 10, 25);
        }

        [Fact]
        public void GetCustomerDetails_CreateCustomerWithOrderTotalLessThan100_ReturnsBasicCustomer()
        {
            _customer.OrderTotal = 10;
            CustomerType result = _customer.GetCustomerDetails();
            Assert.IsType<BasicCustomer>(result);
        }

        [Fact]
        public void GetCustomerDetails_CreateCustomerWithOrderTotalMoreThan100_ReturnsPremiumCustomer()
        {
            _customer.OrderTotal = 200;
            CustomerType result = _customer.GetCustomerDetails();
            Assert.IsType<PremiumCustomer>(result);
        }
    }
}
