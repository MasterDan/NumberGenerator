using System;
using System.Collections.Generic;
using System.Linq;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public IEnumerable<long> Generate(long from, long to)
        {
            var result = new List<double>();
            for (long i = from; i <= to; i++)
            {
                double numb;
                do
                {
                    numb = _random.NextDouble();
                } while (!result.Contains(numb));
                result.Add(numb);
            }

            return result.Select(r => (long) (from + r * (to - from + 1)));
        }
    }
}