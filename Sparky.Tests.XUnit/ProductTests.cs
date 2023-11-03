using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky.Tests.XUnit
{
    public class ProductTests
    {
        [Fact]
        public void GetPrice_IsPremiumCustomer_ReturnsPriceWith20Discount()
        {
            Product product = new() { Price = 100 };

            var result = product.GetPrice(new Customer { IsPremium = true });

            Assert.Equal(80, result);
        }
    }
}
