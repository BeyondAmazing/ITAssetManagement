using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class CategoryFaker : Faker<Category>
    {
        public CategoryFaker()
        {
            RuleFor(x => x.Id, f => f.Random.Guid());
            RuleFor(x => x.Name, f => f.Commerce.Categories(1).First());
        }
    }
}
