namespace Domain.Entities;

public class StatusLabel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty; // e.g., "Deployed", "Retired"
    public bool? Deployable { get; private set; } = false; // Can it be assigned?
    public bool? Pending { get; private set; } = true;
    public bool? Archived { get; private set; } = false;

    private StatusLabel() { }

    public static StatusLabel Create(string name, bool? deployable, bool? pending, bool? archived)
    {
        return new StatusLabel
        {
            Name = name,
            Deployable = deployable,
            Pending = pending,
            Archived = archived
        };
    }
}
