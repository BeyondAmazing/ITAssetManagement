using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("assets");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
        builder.Property(a => a.AssetTag).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Serial).HasMaxLength(255);
        builder.Property(a => a.PurchaseCost).HasColumnType("decimal(18,2)");
        builder.Property(a => a.PurchaseDate).HasColumnType("date");
        builder.Property(a => a.Notes).HasMaxLength(1000);

        // Relationships
        builder.HasOne(a => a.Model)
               .WithMany()
               .HasForeignKey(a => a.ModelId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Status)
               .WithMany()
               .HasForeignKey(a => a.StatusId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Company)
               .WithMany()
               .HasForeignKey(a => a.CompanyId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.AssignedTo)
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Location)
               .WithMany()
               .HasForeignKey(a => a.LocationId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Supplier)
               .WithMany()
               .HasForeignKey(a => a.SupplierId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Depreciation)
               .WithMany()
               .HasForeignKey(a => a.DepreciationId)
               .OnDelete(DeleteBehavior.SetNull);

        // Indexes
        builder.HasIndex(a => a.AssetTag).IsUnique();
        builder.HasIndex(a => a.Serial);
    }
}