using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers;

public class UserFaker : Faker<User>
{
    public UserFaker()
    {
        RuleFor(u => u.Id, f => f.Random.Guid());
        RuleFor(u => u.Username, f => f.Internet.UserName());
        RuleFor(u => u.FirstName, f => f.Name.FirstName());
        RuleFor(u => u.LastName, f => f.Name.LastName());
        RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
        RuleFor(u => u.CompanyId, f => f.Random.Guid());
    }
}
