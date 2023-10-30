using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.NUnit
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_InputTwoInt_ReturnsCorrectAddition()
        {
            // Arrange
            Calculator calculator = new();

            // Act
            var result = calculator.Add(20, 30);

            // Assert
            Assert.AreEqual(50, result);
        }

        [Test]
        [TestCase(11)]
        [TestCase(23)]
        [TestCase(57)]
        public void IsOdd_OddNumber_ReturnsTrue(int a)
        {
            Calculator calc = new();
            bool result = calc.IsOdd(a);

            // Assert.That(result, Is.EqualTo(true));
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOddNumber_EvenNumber_ReturnsTrue()
        {
            Calculator calc = new();
            bool result = calc.IsOdd(4);
            Assert.IsFalse(result);
        }
    }
}
