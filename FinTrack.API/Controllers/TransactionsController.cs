using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;
using System.Security.Claims;

namespace FinTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // JWT token içinden userId okur
    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId!);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();

        var result = await _transactionService.GetAllAsync(userId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto dto)
    {
        var userId = GetUserId();

        var result = await _transactionService.CreateAsync(dto, userId);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateTransactionDto dto)
    {
        var userId = GetUserId();

        var result = await _transactionService.UpdateAsync(id, dto, userId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();

        var success = await _transactionService.DeleteAsync(id, userId);

        if (!success)
            return NotFound();

        return Ok(new { message = "Deleted successfully" });
    }
}