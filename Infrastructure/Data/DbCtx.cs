using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data;

public class DbCtx : DbContext
{
    public DbSet<Asset> Assets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<License> Licenses { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<StatusLabel> StatusLabels { get; set; } = null!;
    public DbSet<AssetModel> AssetModels { get; set; } = null!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<ActionLog> ActionLogs { get; set; } = null!;
    public DbSet<Accessory> Accessories { get; set; } = null!;
    public DbSet<Component> Components { get; set; } = null!;
    public DbSet<Consumable> Consumables { get; set; } = null!;
    public DbSet<Depreciation> Depreciations { get; set; } = null!;
    public DbSet<CustomField> CustomFields { get; set; } = null!;
    public DbSet<CustomFieldSet> CustomFieldSets { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<LicenseSeat> LicenseSeats { get; set; } = null!;
    public DbSet<AssetMaintenance> AssetMaintenances { get; set; } = null!;

    public DbCtx(DbContextOptions<DbCtx> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Automatically apply all configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbCtx).Assembly);
    }
}
