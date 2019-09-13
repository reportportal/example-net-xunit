using ReportPortal.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class ExternalClass
    {
        public async Task InvokeMeAsync()
        {
            await Task.Delay(0);

            Log.Debug("I am invoked");
        }
    }
}
