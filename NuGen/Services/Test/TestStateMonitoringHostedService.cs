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
            var task1 = Task.Run(async () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    //Console.WriteLine($"i: {i}");
                    await Task.Delay(TimeSpan.FromSeconds(0.5), cancellationToken);
                    _state.NumberGenerated();
                }
            }, cancellationToken);
            var task2 = Task.Run(async () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    //Console.WriteLine($"i2: {i}");
                    await Task.Delay(TimeSpan.FromSeconds(1.5), cancellationToken);
                    _state.NumberSaved();
                }
            }, cancellationToken);
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
            Task.WaitAll(task1,task2);
            Console.WriteLine("Tasks Completed");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}