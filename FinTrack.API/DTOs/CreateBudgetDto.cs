namespace FinTrack.API.DTOs;

public class CreateBudgetDto
{
    public int Month { get; set; }

    public int Year { get; set; }

    public decimal LimitAmount { get; set; }
}