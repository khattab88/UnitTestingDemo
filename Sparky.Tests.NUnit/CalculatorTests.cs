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
        private Calculator _calculator = new();

        [Test]
        public void Add_InputTwoInt_ReturnsCorrectAddition()
        {
            // Arrange

            // Act
            var result = _calculator.Add(20, 30);

            // Assert
            Assert.AreEqual(50, result);
        }

        [Test]
        [TestCase(11)]
        [TestCase(23)]
        [TestCase(57)]
        public void IsOdd_OddNumber_ReturnsTrue(int a)
        {
            bool result = _calculator.IsOdd(a);

            // Assert.That(result, Is.EqualTo(true));
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOddNumber_EvenNumber_ReturnsTrue()
        {
            bool result = _calculator.IsOdd(4);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = true)]
        public bool IsOdd_InputNumber_ReturnsTrueIfOdd(int a)
        {
            bool result = _calculator.IsOdd(a);

            return result;
        }

        [Test]
        [TestCase(5.4, 10.5)] //15.9
        [TestCase(5.43, 10.53)] //15.93
        [TestCase(5.49, 10.59)] //16.08
        public void Add_InputTwoDoubles_ReturnsCorrectAddition(double a, double b)
        {
            // Arrange

            // Act
            var result = _calculator.AddDouble(a,b);

            // Assert
            Assert.AreEqual(15.9, result, 1);
        }

        [Test]
        public void GetOddNumbersRange_InputMinAndMax_ReturnsValidOddNumbersRange()
        {
            List<int> expected = new() { 1,3,5,7,9 };

            List<int> result = _calculator.GetOddNumbersRange(1, 10);

            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}
