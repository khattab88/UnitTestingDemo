using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky.Tests.XUnit
{
    public class CalculatorTests
    {
        private Calculator _calculator = new();

        [Fact]
        public void Add_InputTwoInt_ReturnsCorrectAddition()
        {
            // Arrange

            // Act
            var result = _calculator.Add(20, 30);

            // Assert
            Assert.Equal(50, result);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(23)]
        [InlineData(57)]
        public void IsOdd_OddNumber_ReturnsTrue(int a)
        {
            bool result = _calculator.IsOdd(a);

            // Assert.That(result, Is.EqualTo(true));
            Assert.True(result);
        }

        [Fact]
        public void IsOddNumber_EvenNumber_ReturnsTrue()
        {
            bool result = _calculator.IsOdd(4);
            Assert.False(result);
        }

        [Theory]
        [InlineData(10, false)]
        [InlineData(11, true)]
        public void IsOdd_InputNumber_ReturnsTrueIfOdd(int a, bool expected)
        {
            bool result = _calculator.IsOdd(a);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)] //15.9
        [InlineData(5.43, 10.53)] //15.93
        [InlineData(5.49, 10.59)] //16.08
        public void Add_InputTwoDoubles_ReturnsCorrectAddition(double a, double b)
        {
            // Arrange

            // Act
            var result = _calculator.AddDouble(a,b);

            // Assert
            Assert.Equal(15.9, result, 0);
        }

        [Fact]
        public void GetOddNumbersRange_InputMinAndMax_ReturnsValidOddNumbersRange()
        {
            List<int> expected = new() { 1,3,5,7,9 };

            List<int> result = _calculator.GetOddNumbersRange(1, 10);

            Assert.Equal(expected, result);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(5, result.Count);
            Assert.DoesNotContain(6, result);
            Assert.Equal(result.OrderBy(u => u), result);
            //Assert.That(result, Is.Unique);
        }
    }
}
