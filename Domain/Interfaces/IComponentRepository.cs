using Domain.Entities;

namespace Domain.Interfaces;

public interface IComponentRepository : IGenericRepository<Component>
{
    Task AssignToAssetAsync(Guid componentId, Guid assetId, int quantity);
}
