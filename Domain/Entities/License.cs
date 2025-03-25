namespace Domain.Entities;

public class License
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Serial { get; private set; } = string.Empty;
    public int Seats { get; private set; } // Total available seats
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public Guid? SupplierId { get; private set; }
    public Supplier? Supplier { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public decimal PurchaseCost { get; private set; }
    public ICollection<LicenseSeat> SeatsAssigned { get; private set; } = new List<LicenseSeat>();

    private License() { }

    public static License Create(string name, string serial, int seats, Guid? companyId, Guid? supplierId, DateTime? expirationDate, decimal purchaseCost)
    {
        return new License
        {
            Name = name,
            Serial = serial,
            Seats = seats,
            CompanyId = companyId,
            SupplierId = supplierId,
            ExpirationDate = expirationDate,
            PurchaseCost = purchaseCost
        };
    }
}
