namespace Domain.Entities;

public class Setting
{
    public int Id { get; private set; }
    public string Key { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    private Setting() { }
    public static Setting Create(string key, string value)
    {
        return new Setting { Key = key, Value = value };
    }
}
