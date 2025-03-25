using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class LicenseSeatConfiguration : IEntityTypeConfiguration<LicenseSeat>
{
    public void Configure(EntityTypeBuilder<LicenseSeat> builder)
    {
        builder.ToTable("license_seats");
        builder.HasKey(ls => ls.Id);

        builder.Property(ls => ls.ExpirationDate).HasColumnType("date");

        // Relationships
        builder.HasOne(ls => ls.License)
               .WithMany(l => l.SeatsAssigned)
               .HasForeignKey(ls => ls.LicenseId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ls => ls.User)
               .WithMany()
               .HasForeignKey(ls => ls.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(ls => ls.Asset)
               .WithMany()
               .HasForeignKey(ls => ls.AssetId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}