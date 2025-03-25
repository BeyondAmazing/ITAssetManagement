using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccessoryRepository : GenericRepository<Accessory>, IAccessoryRepository
{
    public AccessoryRepository(DbCtx context) : base(context)
    {
    }

    public override async Task<Accessory?> GetByIdAsync(Guid id)
    {
        return await _context.Accessories
            .Include(a => a.Category)
            .Include(a => a.Company)
            .Include(a => a.Manufacturer)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public override async Task<IEnumerable<Accessory>> GetAllAsync()
    {
        return await _context.Accessories
            .Include(a => a.Category)
            .Include(a => a.Company)
            .Include(a => a.Manufacturer)
            .ToListAsync();
    }

    public async Task CheckOutAsync(Guid accessoryId, Guid userId, int quantity)
    {
        var accessory = await GetByIdAsync(accessoryId);
        if (accessory == null) throw new Exception("Accessory not found");
        if (accessory.Quantity < quantity) throw new Exception("Insufficient quantity");

        accessory.Quantity -= quantity;
        await _context.SaveChangesAsync();

        await _context.ActionLogs.AddAsync(ActionLog.Create("checkout", userId, accessoryId, "accessory", $"Checked out {quantity} units"));
        await _context.SaveChangesAsync();
    }
}
