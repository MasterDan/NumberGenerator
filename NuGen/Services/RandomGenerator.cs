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
            var cache = new List<double>();
            for (long i = from; i <= to; i++)
            {
                double numb;
                do
                {
                    numb = _random.NextDouble();
                } while (!cache.Contains(numb));
                cache.Add(numb);
                var newValue = (long) (from + numb * (to - from + 1));
                yield return newValue;
            }
        }
    }
}