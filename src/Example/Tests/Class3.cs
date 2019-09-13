using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Example.XUnit.Tests
{
    public class Class3
    {
        [Fact(DisplayName = "OverridedTestName1", Skip = "my test skip reason")]
        public void Test1()
        {

        }

        [Fact(DisplayName = "OverridedTestName2")]
        public void Test2()
        {
            System.Threading.Thread.Sleep(10000);



            for (int i = 0; i < 10; i++)
            {
                Log.Debug($"From Class3.Test2 log #{i}");
            }
        }
    }
}
