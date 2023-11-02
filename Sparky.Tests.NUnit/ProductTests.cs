using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.NUnit
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_IsPremiumCustomer_ReturnsPriceWith20Discount()
        {
            Product product = new() { Price = 100 };

            var result = product.GetPrice(new Customer { IsPremium = true });

            Assert.That(result, Is.EqualTo(80));
        }
    }
}
