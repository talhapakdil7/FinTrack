namespace FinTrack.API.Entities;

public class Category
{
    // 1. Primary Key
    public int Id { get; set; }

    // 2. Foreign Key
    public int UserId { get; set; }

    // 3. Navigation Property
    public User? User { get; set; }

    // 4. Business Fields
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Income / Expense

    // 5. Audit
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}