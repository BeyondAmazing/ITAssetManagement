namespace Domain.Entities;

public class Component
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public string Serial { get; private set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PurchaseCost { get; private set; }

    private Component() { }
    public static Component Create(string name, Guid categoryId, Guid? companyId, string serial, int quantity, decimal purchaseCost)
    {
        return new Component { Name = name, CategoryId = categoryId, CompanyId = companyId, Serial = serial, Quantity = quantity, PurchaseCost = purchaseCost };
    }
}
