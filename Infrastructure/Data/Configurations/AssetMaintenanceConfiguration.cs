using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class AssetMaintenanceConfiguration : IEntityTypeConfiguration<AssetMaintenance>
{
    public void Configure(EntityTypeBuilder<AssetMaintenance> builder)
    {
        builder.ToTable("asset_maintenances");
        builder.HasKey(am => am.Id);

        builder.Property(am => am.MaintenanceType).IsRequired().HasMaxLength(50);
        builder.Property(am => am.Title).IsRequired().HasMaxLength(255);
        builder.Property(am => am.StartDate).IsRequired().HasColumnType("date");
        builder.Property(am => am.CompletionDate).HasColumnType("date");
        builder.Property(am => am.Cost).HasColumnType("decimal(18,2)");

        // Relationships
        builder.HasOne(am => am.Asset)
               .WithMany()
               .HasForeignKey(am => am.AssetId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(am => am.Supplier)
               .WithMany()
               .HasForeignKey(am => am.SupplierId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}