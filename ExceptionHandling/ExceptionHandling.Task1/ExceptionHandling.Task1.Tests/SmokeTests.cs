using ExceptionHandling.Task1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Assert.True(inputStrings.Go().SequenceEqual(expectedResults));
        }
    }
}
