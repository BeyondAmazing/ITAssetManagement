using Domain.Entities;

namespace Domain.Interfaces;

public interface IAssetRepository : IGenericRepository<Asset>
{
    Task<Asset?> GetByAssetTagAsync(string assetTag);
    Task<IEnumerable<Asset>> GetByStatusIdAsync(Guid statusId);
    Task<IEnumerable<Asset>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<ActionLog>> GetHistoryAsync(Guid assetId);
    Task CheckOutAsync(Guid assetId, Guid userId);
    Task CheckInAsync(Guid assetId);
}
