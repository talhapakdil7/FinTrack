namespace FinTrack.API.DTOs;

public class CategoryReportDto
{
    public int? CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }
}