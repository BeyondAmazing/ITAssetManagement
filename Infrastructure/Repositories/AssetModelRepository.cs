using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AssetModelRepository : GenericRepository<AssetModel>, IAssetModelRepository
{
    public AssetModelRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<AssetModel?> GetByIdAsync(Guid id)
    {
        return await _context.AssetModels
            .Include(am => am.Manufacturer)
            .Include(am => am.Category)
            .FirstOrDefaultAsync(am => am.Id == id);
    }

    public override async Task<IEnumerable<AssetModel>> GetAllAsync()
    {
        return await _context.AssetModels
            .Include(am => am.Manufacturer)
            .Include(am => am.Category)
            .ToListAsync();
    }
}
