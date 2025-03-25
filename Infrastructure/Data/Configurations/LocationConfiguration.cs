using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name).IsRequired().HasMaxLength(255);
        builder.Property(l => l.Address).HasMaxLength(255);
        builder.Property(l => l.City).HasMaxLength(100);
        builder.Property(l => l.State).HasMaxLength(100);
        builder.Property(l => l.Country).HasMaxLength(100);

        // Self-referencing relationship
        builder.HasOne(l => l.Parent)
               .WithMany()
               .HasForeignKey(l => l.ParentId)
               .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(l => l.Name);
    }
}