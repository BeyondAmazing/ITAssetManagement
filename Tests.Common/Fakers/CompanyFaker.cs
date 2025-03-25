using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class CompanyFaker : Faker<Company>
    {
        public CompanyFaker()
        {
            RuleFor(x => x.Id, f => f.Random.Guid());
            RuleFor(x => x.Name, f => f.Company.CompanyName());
            RuleFor(x => x.Email, f => f.Internet.Email());
            RuleFor(x => x.Phone, f => f.Phone.PhoneNumber());
        }
    }
}
