using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class CustomFieldConfiguration : IEntityTypeConfiguration<CustomField>
{
    public void Configure(EntityTypeBuilder<CustomField> builder)
    {
        builder.ToTable("custom_fields");
        builder.HasKey(cf => cf.Id);

        builder.Property(cf => cf.Name).IsRequired().HasMaxLength(255);
        builder.Property(cf => cf.Element).IsRequired().HasMaxLength(50);
        builder.Property(cf => cf.FieldValues).HasMaxLength(1000);
        builder.Property(cf => cf.Format).HasMaxLength(50);

        // Indexes
        builder.HasIndex(cf => cf.Name);
    }
}