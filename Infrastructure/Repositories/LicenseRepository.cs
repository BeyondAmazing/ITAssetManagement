using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LicenseRepository : GenericRepository<License>, ILicenseRepository
{
    public LicenseRepository(DbCtx context) : base(context)
    {

    }

    public override async Task<License?> GetByIdAsync(Guid id)
    {
        return await _context.Licenses
            .Include(l => l.Company)
            .Include(l => l.Supplier)
            .Include(l => l.SeatsAssigned)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public override async Task<IEnumerable<License>> GetAllAsync()
    {
        return await _context.Licenses
            .Include(l => l.Company)
            .Include(l => l.Supplier)
            .Include(l => l.SeatsAssigned)
            .ToListAsync();
    }

    public async Task<IEnumerable<LicenseSeat>> GetSeatsAsync(Guid licenseId)
    {
        return await _context.LicenseSeats
            .Where(ls => ls.LicenseId == licenseId)
            .ToListAsync();
    }

    public async Task AssignSeatAsync(Guid licenseId, Guid? userId, Guid? assetId)
    {
        var seat = LicenseSeat.Create(licenseId, userId, assetId, null);
        await _context.LicenseSeats.AddAsync(seat);
        await _context.SaveChangesAsync();
    }

    public async Task FreeSeatAsync(Guid seatId)
    {
        var seat = await _context.LicenseSeats.FindAsync(seatId);
        if (seat != null)
        {
            _context.LicenseSeats.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}
