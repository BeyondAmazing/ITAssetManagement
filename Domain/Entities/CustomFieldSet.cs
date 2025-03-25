namespace Domain.Entities;

public class CustomFieldSet
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<CustomField> CustomFields { get; private set; } = new List<CustomField>();

    private CustomFieldSet() { }
    public static CustomFieldSet Create(string name)
    {
        return new CustomFieldSet { Name = name };
    }
}
