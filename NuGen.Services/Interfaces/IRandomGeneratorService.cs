using System.Collections.Generic;

namespace NuGen.Services.Interfaces
{
    public interface IRandomGeneratorService
    {
        IAsyncEnumerable<long> GenerateUniqueNumbersAsync(long from, long to);
    }
}