namespace Domain.Entities;

public class Manufacturer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private Manufacturer() { }
    public static Manufacturer Create(string name) => new Manufacturer { Name = name };
}
