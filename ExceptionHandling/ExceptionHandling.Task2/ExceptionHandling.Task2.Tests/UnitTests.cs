using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExceptionHandling.Task2.Tests
{
    public class UnitTests
    {
        [Fact]
        public void OnNullInput_MustThrowArgNullExc()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Core.Core.Parse(null);
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData("\u0007")]
        [InlineData("\u0007\u0007\u0007\u0007")]
        [InlineData(" \u0007 ")]
        public void OnEmptyInput_MustThrowFormatExc(string str)
        {
            Assert.Throws<FormatException>(() =>
            {
                Core.Core.Parse(str);
            });
        }

        [Theory]
        [InlineData("1")]

        [InlineData(" 1")]
        [InlineData("    1")]
        [InlineData("\u00071")]
        [InlineData("\u0007\u0007\u0007\u00071")]

        [InlineData("1 ")]
        [InlineData("1    ")]
        [InlineData("1\u0007")]
        [InlineData("1\u0007\u0007\u0007\u0007")]

        [InlineData(" 1 ")]
        [InlineData("    1    ")]
        [InlineData("\u00071\u0007")]
        [InlineData("\u0007\u0007\u0007\u00071\u0007\u0007\u0007")]

        public void TrimMustWork(string str)
        {
            int actual = Core.Core.Parse(str);

            int expected = 1;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("a1")]
        [InlineData("1a")]
        [InlineData("a123")]
        [InlineData("123a")]
        [InlineData("abc1")]
        [InlineData("1abc")]
        [InlineData("abc123")]
        [InlineData("++1")]
        [InlineData("--1")]
        [InlineData("-+1")]
        public void OnNonDigitInput_MustThrowFormatExc(string str)
        {
            Assert.Throws<FormatException>(() =>
            {
                Core.Core.Parse(str);
            });
        }

        [Theory]

        [InlineData("0", 0)]
        [InlineData("+0", 0)]
        [InlineData("-0", 0)]

        [InlineData("1", 1)]
        [InlineData("+1", 1)]
        [InlineData("-1", -1)]

        [InlineData("123", 123)]
        [InlineData("+123", 123)]
        [InlineData("-123", -123)]
        public void PlusMinusDigit_MustWork(string str, int expected)
        {
            int actual = Core.Core.Parse(str);

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
