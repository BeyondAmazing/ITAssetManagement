using Domain.Entities;

namespace Domain.Interfaces;

public interface IActionLogRepository : IGenericRepository<ActionLog>
{
    Task<IEnumerable<ActionLog>> GetByItemAsync(string itemType, Guid itemId);
}
