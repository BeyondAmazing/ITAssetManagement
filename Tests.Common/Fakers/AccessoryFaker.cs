using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers;

public class AccessoryFaker : Faker<Accessory>
{
    public AccessoryFaker() 
    {
        RuleFor(a => a.Id, f => f.Random.Guid());
        RuleFor(a => a.Name, f => f.Commerce.ProductName());
        RuleFor(a => a.CategoryId, f => f.Random.Guid());
        RuleFor(a => a.CompanyId, f => f.Random.Guid().OrNull(f, 0.2f));
        RuleFor(a => a.Quantity, f => f.Random.Int(1, 100));
    }
}
