namespace Domain.Entities;

public class AssetModel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid? ManufacturerId { get; private set; }
    public Manufacturer? Manufacturer { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    private AssetModel() { }

    public static AssetModel Create(string name, Guid? manufacturerId, Guid? categoryId)
    {
        return new AssetModel
        {
            Name = name,
            ManufacturerId = manufacturerId,
            CategoryId = categoryId
        };
    }
}
