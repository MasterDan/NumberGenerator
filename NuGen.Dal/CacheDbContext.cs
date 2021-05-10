using Microsoft.EntityFrameworkCore;
using NuGen.Dal.Entities;

namespace NuGen.Dal
{
    public sealed class CacheDbContext : DbContext
    {
        public CacheDbContext(DbContextOptions options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<RecordEntity> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<RecordEntity>().Configure();
            base.OnModelCreating(modelBuilder);
        }
    }
}