using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<Location?> GetByIdAsync(Guid id)
    {
        return await _context.Locations
            .Include(l => l.Parent)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public override async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations
            .Include(l => l.Parent)
            .ToListAsync();
    }

    public async Task<IEnumerable<Location>> GetChildrenAsync(Guid parentId)
    {
        return await _context.Locations
            .Where(l => l.ParentId == parentId)
            .ToListAsync();
    }
}
