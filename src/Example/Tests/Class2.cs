using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Example.XUnit.Tests
{
    public class Class2
    {
        private ITestOutputHelper _output;

        public Class2(ITestOutputHelper output)
        {
            _output = output.WithReportPortal();
        }

        [Fact]
        public async Task Test1()
        {
            await new ExternalClass().InvokeMeAsync();

            await Task.Delay(5000);

            for (int i = 0; i < 10; i++)
            {
                Log.Info($"From Class2.Test1 log #{i}");
            }

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() => { Log.Info("Parallel From Class2.Test1 log"); }));
            }

            Task.WaitAll(tasks.ToArray());

            tasks = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                tasks.Add(Task.Run(async () => { await new ExternalClass().InvokeMeAsync(); }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Test2(int p1, int p2)
        {
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Output from Class2.Test2");

            for (int i = 0; i < 10; i++)
            {
                Log.Debug($"From Class2.Test2 log #{i}");
            }
        }
    }
}
