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
        [InlineData("1")] // one U+0007
        [InlineData("1")] // four    U+0007

        // spaces in tail
        [InlineData("1 ")] // one space
        [InlineData("1    ")] // four spaces
        [InlineData("1")] // one U+0007
        [InlineData("1")] // four    U+0007

        //spaces in head and tail
        [InlineData(" 1 ")] // one space
        [InlineData("    1    ")] // four spaces
        [InlineData("1")] // one U+0007
        [InlineData("1")] // four    U+0007
        public void NonPrintableChars_MustBeIgnored(string str)
        {
            int actual = int.Parse(new[] { str }.Go().First().ToString());

            StringBuilder sb = new StringBuilder(str.Length);
            sb.Append(str.Where(c => c.IsPrintable()).ToArray());

            int expected = int.Parse(str);

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
        [InlineData("")]
        [InlineData("")]
        [InlineData("  ")]
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
