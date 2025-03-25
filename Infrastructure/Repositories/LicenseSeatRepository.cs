using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LicenseSeatRepository : GenericRepository<LicenseSeat>, ILicenseSeatRepository
{
    public LicenseSeatRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<LicenseSeat?> GetByIdAsync(Guid id)
    {
        return await _context.LicenseSeats
            .Include(ls => ls.License)
            .Include(ls => ls.User)
            .Include(ls => ls.Asset)
            .FirstOrDefaultAsync(ls => ls.Id == id);
    }

    public override async Task<IEnumerable<LicenseSeat>> GetAllAsync()
    {
        return await _context.LicenseSeats
            .Include(ls => ls.License)
            .Include(ls => ls.User)
            .Include(ls => ls.Asset)
            .ToListAsync();
    }
}
