namespace FinTrack.API.DTOs;

public class BudgetDto
{
    public int Id { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal LimitAmount { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal RemainingAmount { get; set; }

    public decimal UsagePercentage { get; set; }
}