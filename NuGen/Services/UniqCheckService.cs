using System.Linq;
using System.Threading.Tasks;
using NuGen.Dal;
using NuGen.Dal.Entities;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class UniqCheckService : IUniqCheckService
    {
        private readonly CacheDbContext _db;

        public UniqCheckService(CacheDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckUniquenessAsync(long value)
        {
            var isUnique = _db.Records.All(r => r.Value != value);
            if (isUnique)
            {
                _db.Records.Add(new RecordEntity {Value = value});
                await _db.SaveChangesAsync();
            }
            return isUnique;
        }
    }
}