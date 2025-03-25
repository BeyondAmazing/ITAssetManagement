namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public bool IsActive { get; private set; } = true;

    private User() { }

    public static User Create(string username, string firstName, string lastName, string email, Guid? companyId)
    {
        return new User
        {
            Username = username,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CompanyId = companyId
        };
    }
}
