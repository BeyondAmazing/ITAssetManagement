namespace Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private Category() { }
    public static Category Create(string name) => new Category { Name = name };
}
