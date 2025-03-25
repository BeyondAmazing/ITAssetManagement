using Domain.Entities;

namespace Domain.Interfaces;

public interface ILicenseRepository : IGenericRepository<License>
{
    Task<IEnumerable<LicenseSeat>> GetSeatsAsync(Guid licenseId);
    Task AssignSeatAsync(Guid licenseId, Guid? userId, Guid? assetId);
    Task FreeSeatAsync(Guid seatId);
}
