using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssetMaintenanceRepository : GenericRepository<AssetMaintenance>, IAssetMaintenanceRepository
{    
    public AssetMaintenanceRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<AssetMaintenance?> GetByIdAsync(Guid id)
    {
        return await _context.AssetMaintenances
            .Include(am => am.Asset)
            .Include(am => am.Supplier)
            .FirstOrDefaultAsync(am => am.Id == id);
    }

    public override async Task<IEnumerable<AssetMaintenance>> GetAllAsync()
    {
        return await _context.AssetMaintenances
            .Include(am => am.Asset)
            .Include(am => am.Supplier)
            .ToListAsync();
    }

    public async Task<IEnumerable<AssetMaintenance>> GetByAssetIdAsync(Guid assetId)
    {
        return await _context.AssetMaintenances
            .Where(am => am.AssetId == assetId)
            .ToListAsync();
    }
}
