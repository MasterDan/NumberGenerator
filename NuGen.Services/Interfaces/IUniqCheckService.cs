using System.Threading.Tasks;

namespace NuGen.Services.Interfaces
{
    public interface IUniqCheckService
    {
        Task<bool> CheckUniquenessAsync(long value);
    }
}