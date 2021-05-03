using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuGen.Services.Interfaces
{
    public interface IRandomGeneratorService
    {
        IAsyncEnumerable<long> GenerateUniqueNumbersAsync(long from, long to);
    }
}