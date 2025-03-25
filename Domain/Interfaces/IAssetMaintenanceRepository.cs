using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssetMaintenanceRepository : IGenericRepository<AssetMaintenance>
{
    Task<IEnumerable<AssetMaintenance>> GetByAssetIdAsync(Guid assetId);
}
