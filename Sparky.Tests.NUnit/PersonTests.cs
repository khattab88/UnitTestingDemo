﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.NUnit
{
    [TestFixture]
    public class PersonTests
    {
        private Person _person;

        [SetUp]
        public void Setup()
        {
            _person = new Person();
        }

        [Test]
        public void Greet_InputFirstAndLastNames_ReturnsFullNameGreeting()
        {
            var result = _person.Greet("John", "Doe");

            // Assert.AreEqual("Hello, John Doe", result);
            Assert.That(result, Is.EqualTo("Hello, John Doe."));
            Assert.That(result, Does.Contain(","));
            Assert.That(result, Does.Contain("john doe").IgnoreCase);
            Assert.That(result, Does.StartWith("Hello"));
            Assert.That(result, Does.EndWith("."));
            Assert.That(result, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+."));
        }

        [Test]
        public void Greeting_GreetNotCalled_ReturnsNull()
        {
            Assert.IsNull(_person.Greeting);
        }

        [Test]
        public void Greeting_GreetCalled_ReturnsGreeting()
        {
            _person.Greet("John", "Doe");
            Assert.IsNotNull(_person.Greeting);
        }
    }
}