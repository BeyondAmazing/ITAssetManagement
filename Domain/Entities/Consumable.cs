namespace Domain.Entities;

public class Consumable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public int Quantity { get; set; }
    public decimal PurchaseCost { get; private set; }

    private Consumable() { }
    public static Consumable Create(string name, Guid categoryId, Guid? companyId, int quantity, decimal purchaseCost)
    {
        return new Consumable { Name = name, CategoryId = categoryId, CompanyId = companyId, Quantity = quantity, PurchaseCost = purchaseCost };
    }
}
