namespace Domain.Entities;

public class Accessory
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public Guid? ManufacturerId { get; private set; }
    public Manufacturer? Manufacturer { get; private set; }
    public int Quantity { get; set; }
    public DateTime? PurchaseDate { get; private set; }
    public decimal PurchaseCost { get; private set; }

    private Accessory() { }
    public static Accessory Create(string name, Guid categoryId, Guid? companyId, Guid? manufacturerId, int quantity, DateTime? purchaseDate, decimal purchaseCost)
    {
        return new Accessory { Name = name, CategoryId = categoryId, CompanyId = companyId, ManufacturerId = manufacturerId, Quantity = quantity, PurchaseDate = purchaseDate, PurchaseCost = purchaseCost };
    }
}
