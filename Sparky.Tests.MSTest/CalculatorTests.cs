using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.MSTest
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_InputTwoInt_ReturnsCorrectAddition()
        {
            // Arrange
            Calculator calculator = new ();

            // Act
            var result = calculator.Add(20,30);

            // Assert
            Assert.AreEqual(50, result);
        }
    }
}
