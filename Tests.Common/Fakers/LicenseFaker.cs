using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class LicenseFaker : Faker<License>
    {
        public LicenseFaker()
        {
            RuleFor(l => l.Id, f => f.Random.Guid());
            RuleFor(l => l.Name, f => f.Commerce.Product() + " License");
            RuleFor(l => l.Serial, f => f.Random.AlphaNumeric(15).ToUpper());
            RuleFor(l => l.Seats, f => f.Random.Int(1, 50));
            RuleFor(l => l.CompanyId, f => f.Random.Guid().OrNull(f, 0.2f));
            RuleFor(l => l.SupplierId, f => f.Random.Guid().OrNull(f, 0.3f));
        }
    }
}
