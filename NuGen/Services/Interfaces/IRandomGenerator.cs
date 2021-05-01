using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuGen.Services.Interfaces
{
    public interface IRandomGenerator
    {
        IAsyncEnumerable<long> GenerateUniqueNumbersAsync(long from, long to);
    }
}