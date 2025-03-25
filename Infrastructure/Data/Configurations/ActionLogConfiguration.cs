using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class ActionLogConfiguration : IEntityTypeConfiguration<ActionLog>
{
    public void Configure(EntityTypeBuilder<ActionLog> builder)
    {
        builder.ToTable("action_logs");
        builder.HasKey(al => al.Id);

        builder.Property(al => al.ActionType).IsRequired().HasMaxLength(50);
        builder.Property(al => al.ItemType).IsRequired().HasMaxLength(50);
        builder.Property(al => al.ItemId).IsRequired();
        builder.Property(al => al.Note).HasMaxLength(1000);
        builder.Property(al => al.CreatedAt).IsRequired();

        // Relationships
        builder.HasOne(al => al.User)
               .WithMany()
               .HasForeignKey(al => al.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        // Indexes
        builder.HasIndex(al => new { al.ItemType, al.ItemId });
        builder.HasIndex(al => al.CreatedAt);
    }
}