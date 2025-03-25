namespace Domain.Entities;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    private Company() { }

    public static Company Create(string name, string? email, string? phone)
    {
        return new Company
        {
            Name = name,
            Email = email,
            Phone = phone
        };
    }
}
