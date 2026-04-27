namespace FinTrack.API.Entities;
//entity veritababına karşılık gelen sınıf
public class Transaction
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Type { get; set; } = string.Empty; 
    // Income veya Expense olacak

    public DateTime TransactionDate { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}