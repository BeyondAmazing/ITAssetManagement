using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccessoryRepository : IGenericRepository<Accessory>
{
    Task CheckOutAsync(Guid accessoryId, Guid userId, int quantity);
}
