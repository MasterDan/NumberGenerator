using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NuGen.Services.Interfaces;

namespace NuGen.Services.Test
{
    public class TestHostedService : IHostedService
    {
        private readonly IStateMonitoringService _state;
        
        public TestHostedService(IStateMonitoringService state)
        {
            _state = state;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.5), cancellationToken);
                }
            }, cancellationToken);
            Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1.5), cancellationToken);
                }
            }, cancellationToken);
            Console.ReadLine();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}