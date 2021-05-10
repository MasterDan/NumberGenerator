using System;
using System.Threading;
using Microsoft.Extensions.Options;
using NuGen.Options.Start;
using NuGen.Services.Services.Interfaces;

namespace NuGen.Services.Services
{
    public class StateMonitoringService : IStateMonitoringService
    {
        private readonly Mutex _progressMutex = new();
        private readonly long _numbersToGenerate;
        private long _generated;
        private long _saved;
        private readonly IConsoleHelperService _consoleHelper;
        private string _header = "";

        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                WriteProgress();
            }
        }

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

        private int? _cursor = null;

        private void WriteProgress()
        {
            _cursor ??= Console.CursorTop;
            Console.SetCursorPosition(0, _cursor.Value);
            _consoleHelper.OverwriteLine(_header);
            _consoleHelper.OverwriteLine($"Generating: {_consoleHelper.GenerateProgress(_generated, _numbersToGenerate)}");
            _consoleHelper.OverwriteLine($"Saving: {_consoleHelper.GenerateProgress(_saved, _numbersToGenerate)}");
        }
    }
}