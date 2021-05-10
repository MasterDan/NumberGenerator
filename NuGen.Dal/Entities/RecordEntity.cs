using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace NuGen.Dal.Entities
{
    public class RecordEntity
    {
        public Guid Id { get; set; }
        public long Value { get; set; }
    }

    public static class RecordEntityConfigurator
    {
        public static void Configure(this EntityTypeBuilder<RecordEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}