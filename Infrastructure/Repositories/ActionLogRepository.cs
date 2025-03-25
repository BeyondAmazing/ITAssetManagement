using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ActionLogRepository : GenericRepository<ActionLog>, IActionLogRepository
{
    public ActionLogRepository(DbCtx context) : base(context)
    {
    }


    public override async Task<ActionLog?> GetByIdAsync(Guid id)
    {
        return await _context.ActionLogs
            .Include(al => al.User)
            .FirstOrDefaultAsync(al => al.Id == id);
    }

    public override async Task<IEnumerable<ActionLog>> GetAllAsync()
    {
        return await _context.ActionLogs
            .Include(al => al.User)
            .OrderByDescending(al => al.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ActionLog>> GetByItemAsync(string itemType, Guid itemId)
    {
        return await _context.ActionLogs
            .Where(al => al.ItemType == itemType && al.ItemId == itemId)
            .OrderByDescending(al => al.CreatedAt)
            .ToListAsync();
    }
}
