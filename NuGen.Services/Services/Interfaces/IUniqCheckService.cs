using System.Threading.Tasks;

namespace NuGen.Services.Services.Interfaces
{
    public interface IUniqCheckService
    {
        Task<bool> CheckUniquenessAsync(long value);
    }
}