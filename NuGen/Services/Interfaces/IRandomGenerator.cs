using System.Collections.Generic;

namespace NuGen.Services.Interfaces
{
    public interface IRandomGenerator
    {
        IEnumerable<long> Generate(long from, long to);
    }
}