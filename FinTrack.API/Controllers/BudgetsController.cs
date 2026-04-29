using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;

namespace FinTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetsController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId!);
    }

    // POST /api/budgets
    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetDto dto)
    {
        var userId = GetUserId();

        var result = await _budgetService.CreateAsync(dto, userId);

        return Ok(result);
    }

    // GET /api/budgets/current
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent()
    {
        var userId = GetUserId();

        var result = await _budgetService.GetCurrentAsync(userId);

        if (result == null)
            return NotFound(new { message = "Bu ay için bütçe bulunamadı" });

        return Ok(result);
    }
}