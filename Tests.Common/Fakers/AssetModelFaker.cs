using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class AssetModelFaker : Faker<AssetModel>
    {
        public AssetModelFaker()
        {
            RuleFor(m => m.Id, f => f.Random.Guid());
            RuleFor(m => m.Name, f => f.Commerce.ProductName());
            RuleFor(m => m.ManufacturerId, f => f.Random.Guid());
            RuleFor(m => m.CategoryId, f => f.Random.Guid());
        }
    }
}
