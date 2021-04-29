using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace NuGen
{
    public class MainService : IHostedService
    {
        private readonly StartOptions _startOptions;

        public MainService(IOptions<StartOptions> startOptions)
        {
            _startOptions = startOptions.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ValidationResult result = _startOptions.Validate();
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Message);
                return Task.CompletedTask;
            }
            Console.WriteLine($"Started with {_startOptions.From} - {_startOptions.To}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}