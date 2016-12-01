using ExceptionHandling.Task1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExceptionHandling.Task1.Tests
{
    public class UnitTests
    {
        #region positive

        [Theory]
        [InlineData("1")] // 0 space
        [InlineData(" 1")] // 1 space
        [InlineData("    1")] // 4 spaces
        [InlineData("1")] // 1 U+0007
        [InlineData("1")] // 4 U+0007
        public void TrimOnStart_MustWork(string str)
        {
            int actual = new[] { str }.Go().First();
            int expected = int.Parse(str.Trim()); // IsControl

            Assert.Equal(actual: actual, expected: expected);
        }

        #endregion

        #region negative

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
                new string[] { "" }.Go();
            });
        }



        #endregion
    }
}
