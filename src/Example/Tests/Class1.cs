using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            _out = output.WithReportPortal();
        }

        [Fact]
        [Trait("Category", "My Category 1")]
        [Trait("Category", "My Category 2")]
        public void Test1()
        {
            using (var scope = Log.BeginNewScope("qwe"))
            {
                Console.WriteLine("Console Output from Class1.Test1");
                _out.WriteLine("Realtime Output from Class1.Test1");
                _out.WriteLine("One more realtime output from Class1.Test1");

                for (int i = 0; i < 10; i++)
                {
                    Log.Debug($"From Class1.Test1 log #{i}");
                }
            }
        }

        [Fact]
        public void Test2()
        {
            using (var scope = Log.BeginNewScope("abc"))
            {
                scope.Info("abc - in scope");

                using (var scope2 = Log.BeginNewScope("qwe"))
                {
                    scope2.Trace("qwe - in scope2");
                    scope2.Status = ReportPortal.Shared.Logging.LogScopeStatus.Skipped;
                }
            }

            Console.WriteLine("Output from Class1.Test2");
            Assert.False(true);
        }
    }
}
