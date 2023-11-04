using Bongo.Models.ModelValidations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Models.Tests
{
    [TestFixture]
    public class DateInFutureAttributeTests
    {
        [Test]
        public void IsValid_InputDateInFuture_ReturnsTrue() 
        {
            DateInFutureAttribute dateValidator = new(() => DateTime.Now);

            var result = dateValidator.IsValid(DateTime.Now.AddSeconds(100));

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(100, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(-100, ExpectedResult = false)]
        public bool IsValid_InputDate_ReturnsTrueIfDateInFuture(int addedTimeInSeconds)
        {
            DateInFutureAttribute dateValidator = new(() => DateTime.Now);

            return dateValidator.IsValid(DateTime.Now.AddSeconds(addedTimeInSeconds));
        }

        [Test]
        public void IsValid_InputAnyDate_DisplayErrorMessage() 
        {
            DateInFutureAttribute dateValidator = new(() => DateTime.Now);
            Assert.AreEqual("Date must be in the future", dateValidator.ErrorMessage);
        }
    }
}
