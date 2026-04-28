using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;
using System.Security.Claims;

namespace FinTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // JWT token içinden userId okur
    private int GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId!);
    }

    // GET /api/categories
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();

        var result = await _categoryService.GetAllAsync(userId);

        return Ok(result);
    }

    // POST /api/categories
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var userId = GetUserId();

        var result = await _categoryService.CreateAsync(dto, userId);

        return Ok(result);
    }
    [HttpPut("{id}")]
public async Task<IActionResult> Update(int id, CreateCategoryDto dto)
{
    var userId = GetUserId();

    var result = await _categoryService.UpdateAsync(id, dto, userId);

    if (result == null)
        return NotFound();

    return Ok(result);
}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var userId = GetUserId();

    var success = await _categoryService.DeleteAsync(id, userId);

    if (!success)
        return NotFound();

    return Ok(new { message = "Category deleted successfully" });
}
}