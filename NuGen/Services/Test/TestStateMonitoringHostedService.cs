using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NuGen.Services.Interfaces;

namespace NuGen.Services.Test
{
    public class TestStateMonitoringHostedService : IHostedService
    {
        private readonly IStateMonitoringService _state;
        
        public TestStateMonitoringHostedService(IStateMonitoringService state)
        {
            _state = state;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        { 
            Console.WriteLine("Мы в дерьме");
            // Task.Factory.StartNew(async () =>
            // {
            //     for (var i = 0; i < 10; i++)
            //     {
            //         _state.NumberGenerated();
            //         await Task.Delay(TimeSpan.FromSeconds(0.5), cancellationToken);
            //     }
            // }, cancellationToken);
            // Task.Factory.StartNew(async () =>
            // {
            //     for (var i = 0; i < 10; i++)
            //     {
            //         _state.NumberSaved();
            //         await Task.Delay(TimeSpan.FromSeconds(1.5), cancellationToken);
            //     }
            // }, cancellationToken);
            // Task.Factory.StartNew(async () =>
            // {
            //     _state.NumberGenerated();
            //     await Task.Delay(TimeSpan.FromSeconds(1));
            //     _state.NumberGenerated();
            //     await Task.Delay(TimeSpan.FromSeconds(1));
            //     _state.NumberSaved();
            //     await Task.Delay(TimeSpan.FromSeconds(1));
            //     _state.NumberSaved();
            // });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}