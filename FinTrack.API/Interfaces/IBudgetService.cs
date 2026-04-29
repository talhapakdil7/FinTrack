using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

public interface IBudgetService
{
    Task<BudgetDto> CreateAsync(CreateBudgetDto dto, int userId);

    Task<BudgetDto?> GetCurrentAsync(int userId);
}