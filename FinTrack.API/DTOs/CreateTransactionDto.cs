namespace FinTrack.API.DTOs;

public class CreateTransactionDto
{

    public int? CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Type { get; set; } = string.Empty;

    public DateTime TransactionDate { get; set; }

    public string? Note { get; set; }
}