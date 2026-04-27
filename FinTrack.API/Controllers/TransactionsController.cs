using Microsoft.AspNetCore.Mvc;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;

namespace FinTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    // Service'i inject ediyoruz
    private readonly ITransactionService _transactionService;

    // Constructor
    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }


    // GET /api/transactions
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _transactionService.GetAll();
        return Ok(result);
    }

    // POST /api/transactions
    [HttpPost]
    public IActionResult Create(CreateTransactionDto dto)
    {
        var result = _transactionService.Create(dto);
        return Ok(result);
    }

    // PUT /api/transactions/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, CreateTransactionDto dto)
    {
        var result = _transactionService.Update(id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // DELETE /api/transactions/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _transactionService.Delete(id);

        if (!success)
            return NotFound();

        return Ok(new { message = "Deleted successfully" });
    }

}