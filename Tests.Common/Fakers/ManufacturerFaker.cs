using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class ManufacturerFaker : Faker<Manufacturer>
    {
        public ManufacturerFaker()
        {
            CustomInstantiator(f => Manufacturer.Create(f.Company.CompanyName()));
            RuleFor(x => x.Id, f => f.Random.Guid());
        }
    }
}
