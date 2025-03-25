namespace Domain.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string AssetTag { get; private set; } = string.Empty; // Unique identifier
    public string Serial { get; private set; } = string.Empty;
    public Guid? ModelId { get; private set; }
    public AssetModel? Model { get; private set; }
    public Guid? StatusId { get; private set; }
    public StatusLabel? Status { get; private set; }
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public Guid? UserId { get; set; }
    public User? AssignedTo { get; private set; }
    public Guid? LocationId { get; private set; }
    public Location? Location { get; private set; }
    public Guid? SupplierId { get; private set; }
    public Supplier? Supplier { get; private set; }
    public Guid? DepreciationId { get; private set; }
    public Depreciation? Depreciation { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public decimal PurchaseCost { get; private set; }
    public string Notes { get; private set; } = string.Empty;

    public ICollection<Component> Components { get; private set; } = new List<Component>(); // Many-to-many via junction table

    // Private constructor for Immutability
    private Asset() { }

    // Factory method for creation (encapsulates construction)
    public static Asset Create(string name, string assetTag, string serial, Guid? modelId, Guid? statusId, Guid? companyId, Guid? userId, Guid? locationId, Guid? supplierId, Guid? depreciationId, DateTime purchaseDate, decimal purchaseCost, string notes)
    {
        return new Asset
        {
            Name = name,
            AssetTag = assetTag,
            Serial = serial,
            ModelId = modelId,
            StatusId = statusId,
            CompanyId = companyId,
            UserId = userId,
            LocationId = locationId,
            SupplierId = supplierId,
            DepreciationId = depreciationId,
            PurchaseDate = purchaseDate,
            PurchaseCost = purchaseCost,
            Notes = notes
        };
    }
}
