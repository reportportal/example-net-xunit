using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Example.XUnit.Tests
{
    public class Class2
    {
        [Fact]
        public void Test1()
        {

        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Test2(int p1, int p2)
        {
            Console.WriteLine("Output from Class2.Test2");
        }
    }
}
