namespace FinTrack.API.Entities;
//entity veritababına karşılık gelen sınıf
public class Transaction
{
    // 1. Primary Key
    public int Id { get; set; }

    // 2. Foreign Keys
    public int UserId { get; set; }
    public int? CategoryId { get; set; }

    // 3. Navigation Properties
    public User? User { get; set; }
    public Category? Category { get; set; }

    // 4. Business Fields
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public string? Note { get; set; }

    // 5. Audit Fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } // daha hic update edilmemis olabilir o yğzden nullable yaptık
}