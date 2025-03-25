namespace Domain.Entities;

public class Depreciation
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Months { get; private set; }

    private Depreciation() { }
    public static Depreciation Create(string name, int months)
    {
        return new Depreciation { Name = name, Months = months };
    }
}
