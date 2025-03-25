using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class ConsumableConfiguration : IEntityTypeConfiguration<Consumable>
{
    public void Configure(EntityTypeBuilder<Consumable> builder)
    {
        builder.ToTable("consumables");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Quantity).IsRequired();
        builder.Property(c => c.PurchaseCost).HasColumnType("decimal(18,2)");

        // Relationships
        builder.HasOne(c => c.Category)
               .WithMany()
               .HasForeignKey(c => c.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Company)
               .WithMany()
               .HasForeignKey(c => c.CompanyId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}