using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky.Tests.NUnit
{
    [TestFixture]
    public class GradingCalculatorTests
    {
        private GradingCalculator _calc;

        [SetUp]
        public void Setup () 
        {
            _calc = new GradingCalculator ();
        }

        [Test]
        public void GetGrade_GivenScore95AndAttendance90_ReturnsGradeA()
        {
            _calc.Score = 95;
            _calc.AttendancePercentage = 90;

            string result = _calc.GetGrade();

            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        public void GetGrade_GivenScore85AndAttendance90_ReturnsGradeB()
        {
            _calc.Score = 85;
            _calc.AttendancePercentage = 90;

            string result = _calc.GetGrade();

            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void GetGrade_GivenScore65AndAttendance90_ReturnsGradeC()
        {
            _calc.Score = 65;
            _calc.AttendancePercentage = 90;

            string result = _calc.GetGrade();

            Assert.That(result, Is.EqualTo("C"));
        }

        [Test]
        public void GetGrade_GivenScore95AndAttendance65_ReturnsGradeB()
        {
            _calc.Score = 95;
            _calc.AttendancePercentage = 65;

            string result = _calc.GetGrade();

            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GetGrade_GivenFailedScoreAndAttendance_ReturnsGradeF(int score, int attendance)
        {
            _calc.Score = score;
            _calc.AttendancePercentage = attendance;

            string result = _calc.GetGrade();

            Assert.That(result, Is.EqualTo("F"));
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_GivenDifferentScoreAndAttendance_ReturnsValidGrade(int score, int attendance)
        {
            _calc.Score = score;
            _calc.AttendancePercentage = attendance;

            string result = _calc.GetGrade();

            return result;
        }
    }
}
