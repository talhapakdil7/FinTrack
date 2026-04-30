using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FinTrack.API.Interfaces;
using FinTrack.API.DTOs;

namespace FinTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        // JWT içinden userId alıyoruz
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _reportService.GetSummaryAsync(userId);

        return Ok(ApiResponse<ReportDto>.Ok(result));
    }

    [HttpGet("category-summary")]
    public async Task<IActionResult> GetCategorySummary()
    {
        // JWT içinden userId alıyoruz
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var result = await _reportService.GetCategorySummaryAsync(userId);

        return Ok(ApiResponse<IEnumerable<CategoryReportDto>>.Ok(result));
    }
}