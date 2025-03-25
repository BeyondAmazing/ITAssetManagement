using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the DbCtx with SQL Server
        services.AddDbContext<DbCtx>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        // Register repositories
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILicenseRepository, LicenseRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IStatusLabelRepository, StatusLabelRepository>();
        services.AddScoped<IAssetModelRepository, AssetModelRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IActionLogRepository, ActionLogRepository>();
        services.AddScoped<IAccessoryRepository, AccessoryRepository>();
        services.AddScoped<IComponentRepository, ComponentRepository>();
        services.AddScoped<IConsumableRepository, ConsumableRepository>();
        services.AddScoped<IDepreciationRepository, DepreciationRepository>();
        services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
        services.AddScoped<ICustomFieldSetRepository, CustomFieldSetRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ILicenseSeatRepository, LicenseSeatRepository>();
        services.AddScoped<IAssetMaintenanceRepository, AssetMaintenanceRepository>();
    }
}
