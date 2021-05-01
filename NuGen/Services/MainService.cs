using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NuGen.Options.Start;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class MainService : IHostedService
    {
        private readonly StartOptions _startOptions;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IWriterService _writerService;

        public MainService(IOptions<StartOptions> startOptions, IRandomGenerator randomGenerator,
            IWriterService writerService)
        {
            _randomGenerator = randomGenerator;
            _writerService = writerService;
            _startOptions = startOptions.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            ValidationResult result = _startOptions.Validate();
            if (!result.IsSuccess)
            {
                Console.WriteLine(result.Message);
                return;
            }

            Console.WriteLine(
                $"Started with {_startOptions.From} - {_startOptions.To} prefix - {_startOptions.Prefix}");
            var numbers = await _randomGenerator
                .GenerateUniqueNumbersAsync(
                    _startOptions.From ?? throw new Exception("_start options has not been validated"),
                    _startOptions.To ?? throw new Exception("_start options has not been validated")
                ).ToListAsync(cancellationToken: cancellationToken);
            Console.WriteLine("Numbers generated! Saving...");
            await _writerService.SaveAllAsync(numbers);
            Console.WriteLine("Saved!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}