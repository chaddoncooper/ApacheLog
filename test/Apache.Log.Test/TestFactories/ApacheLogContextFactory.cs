using Apache.Log.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Apache.Log.Test.TestFactories
{
    internal interface IApacheLogContextFactory
    {
        ApacheLogContext NewTestContext();
    }

    internal class ApacheLogContextFactory : IApacheLogContextFactory
    {
        public ApacheLogContext NewTestContext()
        {
            var options = new DbContextOptionsBuilder<ApacheLogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApacheLogContext(options);
        }
    }
}
