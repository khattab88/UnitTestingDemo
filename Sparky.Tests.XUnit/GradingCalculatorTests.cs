using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky.Tests.XUnit
{
    public class GradingCalculatorTests
    {
        private GradingCalculator _calc;

        public GradingCalculatorTests() 
        {
            _calc = new GradingCalculator ();
        }

        [Fact]
        public void GetGrade_GivenScore95AndAttendance90_ReturnsGradeA()
        {
            _calc.Score = 95;
            _calc.AttendancePercentage = 90;

            string result = _calc.GetGrade();

            Assert.Equal("A", result);
        }

        [Fact]
        public void GetGrade_GivenScore85AndAttendance90_ReturnsGradeB()
        {
            _calc.Score = 85;
            _calc.AttendancePercentage = 90;

            string result = _calc.GetGrade();

            Assert.Equal("B", result);
        }

        [Fact]
        public void GetGrade_GivenScore65AndAttendance90_ReturnsGradeC()
        {
            _calc.Score = 65;
            _calc.AttendancePercentage = 90;

            string result = _calc.GetGrade();

            Assert.Equal("C", result);
        }

        [Fact]
        public void GetGrade_GivenScore95AndAttendance65_ReturnsGradeB()
        {
            _calc.Score = 95;
            _calc.AttendancePercentage = 65;

            string result = _calc.GetGrade();

            Assert.Equal("B", result);
        }

        [Theory]
        [InlineData(95, 55)]
        [InlineData(65, 55)]
        [InlineData(50, 90)]
        public void GetGrade_GivenFailedScoreAndAttendance_ReturnsGradeF(int score, int attendance)
        {
            _calc.Score = score;
            _calc.AttendancePercentage = attendance;

            string result = _calc.GetGrade();

            Assert.Equal("F", result);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GetGrade_GivenDifferentScoreAndAttendance_ReturnsValidGrade(int score, int attendance, string expected)
        {
            _calc.Score = score;
            _calc.AttendancePercentage = attendance;

            string result = _calc.GetGrade();

            Assert.Equal(expected, result);
        }
    }
}
