using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Example.XUnit.Tests
{
    [Collection("Overrided Collection Name for Class1")]
    public class Class1
    {
        private ITestOutputHelper _out;

        public Class1(ITestOutputHelper output)
        {
            _out = output;
        }

        [Fact]
        [Trait("Category", "My Category 1")]
        [Trait("Category", "My Category 2")]
        public void Test1()
        {
            Console.WriteLine("Output from Class1.Test1");
            _out.WriteLine("Realtime Output from Class1.Test1");
            _out.WriteLine("One more realtime output from Class1.Test1");
        }

        [Fact]
        public void Test2()
        {
            Console.WriteLine("Output from Class1.Test2");
            Assert.False(true);
        }
    }
}
