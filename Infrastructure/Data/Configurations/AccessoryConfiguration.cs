using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
{
    public void Configure(EntityTypeBuilder<Accessory> builder)
    {
        builder.ToTable("accessories");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(255);
        builder.Property(a => a.Quantity).IsRequired();
        builder.Property(a => a.PurchaseCost).HasColumnType("decimal(18,2)");
        builder.Property(a => a.PurchaseDate).HasColumnType("date");

        // Relationships
        builder.HasOne(a => a.Category)
               .WithMany()
               .HasForeignKey(a => a.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Company)
               .WithMany()
               .HasForeignKey(a => a.CompanyId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Manufacturer)
               .WithMany()
               .HasForeignKey(a => a.ManufacturerId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}