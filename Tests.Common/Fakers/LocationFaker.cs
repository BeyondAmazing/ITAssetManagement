using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class LocationFaker : Faker<Location>
    {
        public LocationFaker()
        {
            RuleFor(l => l.Id, f => f.Random.Guid());
            RuleFor(l => l.Name, f => f.Address.City() + " Office");
            RuleFor(l => l.ParentId, f => f.Random.Guid().OrNull(f, 0.4f));
        }
    }
}
