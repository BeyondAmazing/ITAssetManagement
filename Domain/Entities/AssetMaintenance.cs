namespace Domain.Entities;

public class AssetMaintenance
{
    public Guid Id { get; private set; }
    public Guid AssetId { get; private set; }
    public Asset Asset { get; private set; } = null!;
    public Guid? SupplierId { get; private set; }
    public Supplier? Supplier { get; private set; }
    public string MaintenanceType { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime? CompletionDate { get; private set; }
    public decimal Cost { get; private set; }

    private AssetMaintenance() { }
    public static AssetMaintenance Create(Guid assetId, Guid? supplierId, string maintenanceType, string title, DateTime startDate, DateTime? completionDate, decimal cost)
    {
        return new AssetMaintenance { AssetId = assetId, SupplierId = supplierId, MaintenanceType = maintenanceType, Title = title, StartDate = startDate, CompletionDate = completionDate, Cost = cost };
    }
}
