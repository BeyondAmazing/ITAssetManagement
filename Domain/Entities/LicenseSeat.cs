namespace Domain.Entities;

public class LicenseSeat
{
    public Guid Id { get; private set; }
    public Guid LicenseId { get; private set; }
    public License License { get; private set; } = null!;
    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    public Guid? AssetId { get; private set; }
    public Asset? Asset { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    private LicenseSeat() { }
    public static LicenseSeat Create(Guid licenseId, Guid? userId, Guid? assetId, DateTime? expirationDate)
    {
        return new LicenseSeat { LicenseId = licenseId, UserId = userId, AssetId = assetId, ExpirationDate = expirationDate };
    }
}
