using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.ToTable("components");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Serial).HasMaxLength(255);
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