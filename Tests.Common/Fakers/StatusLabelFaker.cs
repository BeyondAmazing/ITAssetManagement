using Bogus;
using Domain.Entities;

namespace Tests.Common.Fakers
{
    public class StatusLabelFaker : Faker<StatusLabel>
    {
        public StatusLabelFaker()
        {
            RuleFor(s => s.Id, f => f.Random.Guid());
            RuleFor(s => s.Name, f => f.PickRandom("Deployable", "Pending", "Broken", "Retired"));
            RuleFor(s => s.Deployable, f => f.Random.Bool());
            RuleFor(s => s.Pending, f => f.Random.Bool());
            RuleFor(s => s.Archived, f => f.Random.Bool());
        }
    }
}
