using System.Collections.Generic;
using System.Threading.Tasks;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class SimpleUniqCheckService : IUniqCheckService
    {
        private readonly List<long> _cache = new();

        public Task<bool> CheckUniquenessAsync(long value)
        {
            if (!_cache.Contains(value))
            {
                _cache.Add(value);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}