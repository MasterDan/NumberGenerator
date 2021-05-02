using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new();
        private readonly IUniqCheckService _check;

        public RandomGenerator(IUniqCheckService check)
        {
            _check = check;
        }

        public async IAsyncEnumerable<long> GenerateUniqueNumbersAsync(long from, long to)
        {
            for (long i = from; i <= to; i++)
            {
                while (true)
                {
                    long valueToAdd = _random.Next(0, 999999);
                    if (!await _check.CheckUniquenessAsync(valueToAdd))
                    {
                        yield return valueToAdd;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}