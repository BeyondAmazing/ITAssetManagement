namespace Domain.Entities;

public class CustomField
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Element { get; private set; } = string.Empty; // e.g., "text", "checkbox"
    public string? FieldValues { get; private set; } // For dropdowns
    public string Format { get; private set; } = string.Empty;

    private CustomField() { }
    public static CustomField Create(string name, string element, string? fieldValues, string format)
    {
        return new CustomField { Name = name, Element = element, FieldValues = fieldValues, Format = format };
    }
}
