using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class CustomFieldsetConfiguration : IEntityTypeConfiguration<CustomFieldSet>
{
    public void Configure(EntityTypeBuilder<CustomFieldSet> builder)
    {
        builder.ToTable("custom_fieldsets");
        builder.HasKey(cf => cf.Id);

        builder.Property(cf => cf.Name).IsRequired().HasMaxLength(255);

        // Many-to-many with CustomField
        builder.HasMany(cf => cf.CustomFields)
               .WithMany()
               .UsingEntity(j => j.ToTable("custom_field_custom_fieldset"));

        // Indexes
        builder.HasIndex(cf => cf.Name).IsUnique();
    }
}