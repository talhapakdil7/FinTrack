namespace FinTrack.API.Entities;

public class Category
{
    public int Id { get; set; }

    // Örn: Market, Maaş, Kira
    public string Name { get; set; } = string.Empty;

    // Income veya Expense
    public string Type { get; set; } = string.Empty;

    // Kategori hangi kullanıcıya ait?
    public int UserId { get; set; }

    public User? User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}