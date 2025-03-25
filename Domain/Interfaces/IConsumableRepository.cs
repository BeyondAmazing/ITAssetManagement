using Domain.Entities;

namespace Domain.Interfaces;

public interface IConsumableRepository : IGenericRepository<Consumable>
{
    Task CheckOutAsync(Guid consumableId, Guid userId, int quantity);
}
