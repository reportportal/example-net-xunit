using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Example.XUnit.Tests
{
    public class Class3
    {
        [Fact(DisplayName = "OverridedTestName1", Skip = "my test skip reason")]
        public void Test1()
        {

        }
    }
}
