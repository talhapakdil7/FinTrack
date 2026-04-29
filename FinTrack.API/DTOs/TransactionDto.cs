namespace FinTrack.API.DTOs;

public class TransactionDto
{


    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? CategoryName { get; set; }
    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Type { get; set; } = string.Empty;

    public DateTime TransactionDate { get; set; }

    public string? Note { get; set; } //null olabılır demek

    public DateTime CreatedAt { get; set; }
}