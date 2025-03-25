namespace Domain.Entities;

public class Supplier
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }

    private Supplier() { }
    public static Supplier Create(string name, string? address, string? phone, string? email)
    {
        return new Supplier { Name = name, Address = address, Phone = phone, Email = email };
    }
}
