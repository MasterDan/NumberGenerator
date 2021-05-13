using System;
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
        private readonly IRandomGeneratorService _randomGeneratorService;
        private readonly IWriterService _writerService;
        private readonly IStateMonitoringService _state;

        public MainService(
            IOptions<StartOptions> startOptions,
            IRandomGeneratorService randomGeneratorService,
            IWriterService writerService, 
            IStateMonitoringService state
            )
        {
            _randomGeneratorService = randomGeneratorService;
            _writerService = writerService;
            _state = state;
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

            _state.Header = $"Started with {_startOptions.From} - {_startOptions.To}";
            var numbers = await _randomGeneratorService
                .GenerateUniqueNumbersAsync(
                    _startOptions.From ?? throw new Exception("_start options has not been validated"),
                    _startOptions.To ?? throw new Exception("_start options has not been validated")
                ).ToListAsync();
            await _writerService.SaveAllAsync(numbers);
            Console.ReadLine();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}