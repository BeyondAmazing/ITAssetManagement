using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ComponentRepository : GenericRepository<Component>, IComponentRepository
{
    public ComponentRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<Component?> GetByIdAsync(Guid id)
    {
        return await _context.Components
            .Include(c => c.Category)
            .Include(c => c.Company)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<IEnumerable<Component>> GetAllAsync()
    {
        return await _context.Components
            .Include(c => c.Category)
            .Include(c => c.Company)
            .ToListAsync();
    }

    public async Task AssignToAssetAsync(Guid componentId, Guid assetId, int quantity)
    {
        var component = await GetByIdAsync(componentId);
        if (component == null) throw new Exception("Component not found");
        if (component.Quantity < quantity) throw new Exception("Insufficient quantity");

        component.Quantity -= quantity;
        await _context.SaveChangesAsync();

        await _context.ActionLogs.AddAsync(ActionLog.Create("assign", null, componentId, "component", $"Assigned {quantity} units to asset {assetId}"));
        await _context.SaveChangesAsync();
    }
}
