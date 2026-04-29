using FinTrack.API.DTOs;

namespace FinTrack.API.Interfaces;

public interface IReportService
{
    Task<ReportDto> GetSummaryAsync(int userId);

    Task<IEnumerable<CategoryReportDto>> GetCategorySummaryAsync(int userId);
}