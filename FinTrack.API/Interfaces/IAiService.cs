using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

public interface IAiService
{
    Task<AiInsightDto> GenerateFinancialInsightAsync(int userId);
}