using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;
public class LicenseConfiguration : IEntityTypeConfiguration<License>
{
    public void Configure(EntityTypeBuilder<License> builder)
    {
        builder.ToTable("licenses");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name).IsRequired().HasMaxLength(255);
        builder.Property(l => l.Serial).HasMaxLength(255);
        builder.Property(l => l.Seats).IsRequired();
        builder.Property(l => l.PurchaseCost).HasColumnType("decimal(18,2)");
        builder.Property(l => l.ExpirationDate).HasColumnType("date");

        // Relationships
        builder.HasOne(l => l.Company)
               .WithMany()
               .HasForeignKey(l => l.CompanyId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(l => l.Supplier)
               .WithMany()
               .HasForeignKey(l => l.SupplierId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(l => l.SeatsAssigned)
               .WithOne(ls => ls.License)
               .HasForeignKey(ls => ls.LicenseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}