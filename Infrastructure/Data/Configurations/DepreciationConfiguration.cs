using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class DepreciationConfiguration : IEntityTypeConfiguration<Depreciation>
{
    public void Configure(EntityTypeBuilder<Depreciation> builder)
    {
        builder.ToTable("depreciations");
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name).IsRequired().HasMaxLength(255);
        builder.Property(d => d.Months).IsRequired();

        // Indexes
        builder.HasIndex(d => d.Name).IsUnique();
    }
}