using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssetRepository : GenericRepository<Asset>, IAssetRepository
{
    public AssetRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<Asset?> GetByIdAsync(Guid id)
    {
        return await _context.Assets
            .Include(a => a.Model)
            .Include(a => a.Status)
            .Include(a => a.Company)
            .Include(a => a.AssignedTo)
            .Include(a => a.Location)
            .Include(a => a.Supplier)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public override async Task<IEnumerable<Asset>> GetAllAsync()
    {
        return await _context.Assets
            .Include(a => a.Model)
            .Include(a => a.Status)
            .Include(a => a.Company)
            .Include(a => a.AssignedTo)
            .Include(a => a.Location)
            .Include(a => a.Supplier)
            .ToListAsync();
    }

    public async Task<Asset?> GetByAssetTagAsync(string assetTag)
    {
        return await _context.Assets
            .Include(a => a.Model)
            .Include(a => a.Status)
            .Include(a => a.Company)
            .Include(a => a.AssignedTo)
            .Include(a => a.Location)
            .Include(a => a.Supplier)
            .FirstOrDefaultAsync(a => a.AssetTag == assetTag);
    }

    public async Task<IEnumerable<Asset>> GetByStatusIdAsync(Guid statusId)
    {
        return await _context.Assets
            .Where(a => a.StatusId == statusId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Asset>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Assets
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ActionLog>> GetHistoryAsync(Guid assetId)
    {
        return await _context.ActionLogs
            .Where(al => al.ItemType == "asset" && al.ItemId == assetId)
            .OrderByDescending(al => al.CreatedAt)
            .ToListAsync();
    }

    public async Task CheckOutAsync(Guid assetId, Guid userId)
    {
        var asset = await GetByIdAsync(assetId);
        if (asset == null) 
            throw new Exception("Asset not found");

        asset.UserId = userId;
        await _context.SaveChangesAsync();

        await _context.ActionLogs.AddAsync(ActionLog.Create("checkout", null, assetId, "asset", $"Checked out to user {userId}"));
        await _context.SaveChangesAsync();
    }

    public async Task CheckInAsync(Guid assetId)
    {
        var asset = await GetByIdAsync(assetId);
        if (asset == null) throw new Exception("Asset not found");

        asset.UserId = null;
        await _context.SaveChangesAsync();

        await _context.ActionLogs.AddAsync(ActionLog.Create("checkin", null, assetId, "asset", "Checked in"));
        await _context.SaveChangesAsync();
    }
}
