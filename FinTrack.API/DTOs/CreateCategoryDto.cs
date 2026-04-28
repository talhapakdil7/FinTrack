namespace FinTrack.API.DTOs;

public class CreateCategoryDto
{
    // Örn: Market, Maaş, Kira
    public string Name { get; set; } = string.Empty;

    // Income veya Expense
    public string Type { get; set; } = string.Empty;
}