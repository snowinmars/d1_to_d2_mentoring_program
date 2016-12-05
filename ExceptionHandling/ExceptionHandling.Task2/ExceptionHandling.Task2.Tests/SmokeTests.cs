using System.Linq;
using Xunit;

namespace ExceptionHandling.Task2.Tests
{
    public class SmokeTests
    {
        [Fact]
        public void A()
        {
            int[] ints = { 0, -1, 1, 1234, -1234, };
            string[] strings = ints.Select(i => i.ToString()).ToArray();

            for (int i = 0; i < ints.Length; i++)
            {
                Assert.Equal(expected: ints[i], actual: Core.Core.Parse(strings[i]));
            }
        }
    }
}