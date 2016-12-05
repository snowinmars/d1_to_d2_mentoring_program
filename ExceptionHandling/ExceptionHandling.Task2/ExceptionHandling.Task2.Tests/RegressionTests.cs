using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExceptionHandling.Task2.Tests
{
    public class RegressionTests
    {
        [Fact]
        public void HowToNameRegressionTests()
        {
            string s1 = $"+{int.MaxValue - 10}";
            int v1 = Core.Core.Parse(s1);

            string s2 = $"{int.MaxValue - 10}";
            int v2 = Core.Core.Parse(s2);

            Assert.Equal(v1, v2);
        }
    }
}
