using ExceptionHandling.Task1.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExceptionHandling.Task1.Tests
{
    public class SmokeTests
    {
        [Fact]
        public void Smoke()
        {
            IEnumerable<string> inputStrings = new[]
            {
                "a",
                "bike",
                "cast",
                "doordoordoor",
            };

            IEnumerable<char> expectedResults = new[] { 'a', 'b', 'c', 'd' };

            Assert.True(inputStrings.CutOff().SequenceEqual(expectedResults));
        }
    }
}