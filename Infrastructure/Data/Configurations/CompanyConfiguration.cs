using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Email).HasMaxLength(255);
        builder.Property(c => c.Phone).HasMaxLength(50);

        // Indexes
        builder.HasIndex(c => c.Name).IsUnique();
    }
}