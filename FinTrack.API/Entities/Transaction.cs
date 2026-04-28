namespace FinTrack.API.Entities;
//entity veritababına karşılık gelen sınıf
public class Transaction
{
    public int Id { get; set; }
    // Bu transaction hangi kullanıcıya ait?
public int UserId { get; set; }

// Bu transaction hangi kategoriye ait?
public int? CategoryId { get; set; }

// Navigation property
public Category? Category { get; set; }

// Navigation property
// EF Core ilişki kurmak için kullanır
public User? User { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Type { get; set; } = string.Empty; 
    // Income veya Expense olacak

    public DateTime TransactionDate { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}