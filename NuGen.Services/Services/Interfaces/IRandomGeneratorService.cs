using System.Collections.Generic;

namespace NuGen.Services.Services.Interfaces
{
    public interface IRandomGeneratorService
    {
        IAsyncEnumerable<long> GenerateUniqueNumbersAsync(long from, long to);
    }
}