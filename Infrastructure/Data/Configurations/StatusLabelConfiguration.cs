using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class StatusLabelConfiguration : IEntityTypeConfiguration<StatusLabel>
{
    public void Configure(EntityTypeBuilder<StatusLabel> builder)
    {
        builder.ToTable("status_labels");
        builder.HasKey(sl => sl.Id);

        builder.Property(sl => sl.Name).IsRequired().HasMaxLength(255);
        builder.Property(sl => sl.Deployable).IsRequired();
        builder.Property(sl => sl.Pending).IsRequired();
        builder.Property(sl => sl.Archived).IsRequired();

        // Indexes
        builder.HasIndex(sl => sl.Name).IsUnique();
    }
}