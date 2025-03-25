using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers;

public class AssetFaker : Faker<Asset>
{
    public AssetFaker()
    {
        RuleFor(a => a.Name, f => f.Commerce.ProductName());
        RuleFor(a => a.AssetTag, f => $"TAG{f.Random.Number(1000, 9999)}");
        RuleFor(a => a.Serial, f => f.Random.AlphaNumeric(10).ToUpper());
        RuleFor(a => a.ModelId, f => f.Random.Guid());
        RuleFor(a => a.StatusId, f => f.Random.Guid());
        RuleFor(a => a.CompanyId, f => f.Random.Guid());
        RuleFor(a => a.LocationId, f => f.Random.Guid());
        RuleFor(a => a.PurchaseDate, f => f.Date.Past(5));
        RuleFor(a => a.PurchaseCost, f => f.Random.Decimal(100, 5000));
        RuleFor(a => a.Notes, f => null);
    }
}
