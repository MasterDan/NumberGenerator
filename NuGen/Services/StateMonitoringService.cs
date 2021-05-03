using System;
using System.Linq;
using NuGen.Options.Start;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class StateMonitoringService : IStateMonitoringService
    {
        private readonly StartOptions _options;
        private readonly long _numbersToGenerate;
        private long _generated;
        private long _saved;

        public StateMonitoringService(StartOptions options)
        {
            _options = options;
            _numbersToGenerate = _options.To.Value  - _options.From.Value;
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

        private string generateProgress(long number, long of)
        {
            if (number == of)
                return "DONE";
            var percents = Math.Floor((double) (number / of));
            var decade = (int) percents * 10;
            var done = Enumerable.Range(0, decade).Select((_)=>"#");
            var remains = Enumerable.Range(0, 10-decade).Select((_)=>" ");
            return $"[ {number} / {of} ] [ {string.Join("", done)}{string.Join("", remains)} ] [ {percents:P} ]";

        }
        private void WriteProgress()
        {
            ClearCurrentConsoleLine();
            Console.WriteLine($"Generating: {generateProgress(_generated,_numbersToGenerate)}");
            ClearCurrentConsoleLine();
            Console.WriteLine($"Saving: {generateProgress(_generated,_numbersToGenerate)}");
        }
    }
}