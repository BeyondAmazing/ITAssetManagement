namespace Domain.Entities;

public class ActionLog
{
    public Guid Id { get; private set; }
    public string ActionType { get; private set; } = string.Empty;
    public Guid? UserId { get; private set; }
    public User? User { get; private set; }
    public Guid ItemId { get; private set; } // Polymorphic ID
    public string ItemType { get; private set; } = string.Empty; // e.g., "asset", "license"
    public string Note { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private ActionLog() { }
    public static ActionLog Create(string actionType, Guid? userId, Guid itemId, string itemType, string note)
    {
        return new ActionLog { ActionType = actionType, UserId = userId, ItemId = itemId, ItemType = itemType, Note = note, CreatedAt = DateTime.UtcNow };
    }
}
