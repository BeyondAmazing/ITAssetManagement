using Domain.Entities;

namespace Domain.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<IEnumerable<Location>> GetChildrenAsync(Guid parentId);
}
