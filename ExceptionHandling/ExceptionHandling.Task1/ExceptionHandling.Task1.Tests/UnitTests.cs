using ExceptionHandling.Task1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using ExceptionHandling.Task1.Extensions;
using Xunit;

namespace ExceptionHandling.Task1.Tests
{
    public class UnitTests
    {
        #region positive

        [Theory]
        [InlineData("1")] // zero spaces

        // spaces in head
        [InlineData(" 1")] // one space
        [InlineData("    1")] // four spaces
        [InlineData("\u00071")] // one U+0007
        [InlineData("\u0007\u0007\u0007\u00071")] // four    U+0007

        // spaces in tail
        [InlineData("1 ")] // one space
        [InlineData("1    ")] // four spaces
        [InlineData("1\u0007")] // one U+0007
        [InlineData("1\u0007\u0007\u0007\u0007")] // four    U+0007

        //spaces in head and tail
        [InlineData(" 1 ")] // one space
        [InlineData("    1    ")] // four spaces
        [InlineData("\u00071\u0007")] // one U+0007
        [InlineData("\u0007\u0007\u0007\u00071\u0007\u0007\u0007\u0007")] // four    U+0007
        public void NonPrintableChars_MustBeIgnored(string str)
        {
            char actual = new[] { str }.Go().First();

            char expected = '1';

            Assert.Equal(actual: actual, expected: expected);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("abcd")]
        public void Print_MustWork(string str)
        {
            Assert.Equal(expected: str[0], actual: new[] {str}.Go().First());
        }

        #endregion

        #region negative

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        [InlineData("\u0007")]
        [InlineData("\u0007\u0007\u0007\u0007")]
        [InlineData(" \u0007\u0007\u0007\u0007 ")]
        public void EmptyStringAreEquals(string str)
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                string[] arr = { str };
                arr.Go();
            });
        }

        [Fact]
        public void OnNullInput_MustThrow_ArgNullExc()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
                new string[] { null }.Go();
            });
        }

        [Fact]
        public void OnEmptyInput_MustThrow_IndexOutOfRangeExc()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                new[] { "" }.Go();
            });

            Assert.Throws<IndexOutOfRangeException>(() => // TODO
            {
                new string[] { }.Go();
            });
        }



        #endregion
    }
}
