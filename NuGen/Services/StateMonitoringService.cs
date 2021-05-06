using System;
using System.Linq;
using Microsoft.Extensions.Options;
using NuGen.Options.Start;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class StateMonitoringService : IStateMonitoringService
    {
        private readonly long _numbersToGenerate;
        private long _generated;
        private long _saved;
        private readonly IConsoleHelperService _consoleHelper;
        

        public StateMonitoringService(IOptions<StartOptions> options, IConsoleHelperService consoleHelper)
        {
            _consoleHelper = consoleHelper;
            var optionsValue = options.Value;
            _numbersToGenerate = optionsValue.To.Value - optionsValue.From.Value + 1;
        }

        public void NumberGenerated()
        {
            if (_generated < _numbersToGenerate)
            {
                _generated++;
            }

            WriteProgress();
        }

        public void NumberSaved()
        {
            if (_saved < _numbersToGenerate)
            {
                _saved++;
            }

            WriteProgress();
        }

        private void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }



        private void WriteProgress()
        {
            Console.WriteLine($"Generating: {_consoleHelper.GenerateProgress(_generated, _numbersToGenerate)}");
            Console.WriteLine($"Saving: {_consoleHelper.GenerateProgress(_generated, _numbersToGenerate)}");
        }
    }
}