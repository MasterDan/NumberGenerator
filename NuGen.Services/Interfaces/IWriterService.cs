using System.Collections.Generic;
using System.Threading.Tasks;

namespace NuGen.Services.Interfaces
{
    public interface IWriterService
    {
        Task SaveAllAsync(IEnumerable<long> array);
        Task SaveAllAsync(IAsyncEnumerable<long> array);
    }
}