using System;
using System.Linq;
using NuGen.Services.Services.Interfaces;

namespace NuGen.Services.Services
{
    public class ConsoleHelperService : IConsoleHelperService
    {
        public string GenerateProgress(long number, long of)
        {
            if (number == of)
                return "DONE";
            var percents =  number / (double) of;
            var decade = (int) Math.Floor(percents * 10);
            var done = Enumerable.Range(0, decade).Select((_) => "#");
            var remains = Enumerable.Range(0, 10 - decade).Select((_) => "_");
            return $" {number}/{of} | {string.Join("", done)}{string.Join("", remains)} | {percents*100:00} % ";
        }

        public void OverwriteLine(string message)
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0,Console.CursorTop-1);
            Console.WriteLine(message);
        }
    }
}