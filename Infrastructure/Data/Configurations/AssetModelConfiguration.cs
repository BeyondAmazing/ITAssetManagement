using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class AssetModelConfiguration : IEntityTypeConfiguration<AssetModel>
{
    public void Configure(EntityTypeBuilder<AssetModel> builder)
    {
        builder.ToTable("asset_models");
        builder.HasKey(am => am.Id);

        builder.Property(am => am.Name).IsRequired().HasMaxLength(255);

        // Relationships
        builder.HasOne(am => am.Manufacturer)
               .WithMany()
               .HasForeignKey(am => am.ManufacturerId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(am => am.Category)
               .WithMany()
               .HasForeignKey(am => am.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}