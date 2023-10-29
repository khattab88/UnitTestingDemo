﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
