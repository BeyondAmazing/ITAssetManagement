using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ConsumableRepository : GenericRepository<Consumable>, IConsumableRepository
{
    public ConsumableRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<Consumable?> GetByIdAsync(Guid id)
    {
        return await _context.Consumables
            .Include(c => c.Category)
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<IEnumerable<Consumable>> GetAllAsync()
    {
        return await _context.Consumables
            .Include(c => c.Category)
            .Include(c => c.Company)
            .ToListAsync();
    }

    public async Task CheckOutAsync(Guid consumableId, Guid userId, int quantity)
    {
        var consumable = await GetByIdAsync(consumableId);
        if (consumable == null) throw new Exception("Consumable not found");
        if (consumable.Quantity < quantity) throw new Exception("Insufficient quantity");

        consumable.Quantity -= quantity;
        await _context.SaveChangesAsync();

        await _context.ActionLogs.AddAsync(ActionLog.Create("checkout", userId, consumableId, "consumable", $"Checked out {quantity} units"));
        await _context.SaveChangesAsync();
    }
}
