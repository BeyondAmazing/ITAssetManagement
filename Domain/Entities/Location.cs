namespace Domain.Entities;

public class Location
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public Guid? ParentId { get; private set; }
    public Location? Parent { get; private set; }

    private Location() { }
    public static Location Create(string name, string? address, string? city, string? state, string? country, Guid? parentId)
    {
        return new Location { Name = name, Address = address, City = city, State = state, Country = country, ParentId = parentId };
    }
}
