using Microsoft.EntityFrameworkCore;
using FinTrack.API.Data;
using FinTrack.API.DTOs;
using FinTrack.API.Entities;
using FinTrack.API.Interfaces;

namespace FinTrack.API.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    // Giriş yapan kullanıcının kategorilerini getirir
    public async Task<IEnumerable<CategoryDto>> GetAllAsync(int userId)
    {
        return await _context.Categories
            .Where(c => c.UserId == userId)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    // Giriş yapan kullanıcı için yeni kategori oluşturur
    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, int userId)
    {
        var category = new Category
        {
            Name = dto.Name,
            Type = dto.Type,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            CreatedAt = category.CreatedAt
        };
    }
    public async Task<CategoryDto?> UpdateAsync(int id, CreateCategoryDto dto, int userId)
{
    var category = await _context.Categories
        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

    if (category == null)
        return null;

    category.Name = dto.Name;
    category.Type = dto.Type;

    await _context.SaveChangesAsync();

    return new CategoryDto
    {
        Id = category.Id,
        Name = category.Name,
        Type = category.Type,
        CreatedAt = category.CreatedAt
    };
}

public async Task<bool> DeleteAsync(int id, int userId)
{
    var category = await _context.Categories
        .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

    if (category == null)
        return false;

    _context.Categories.Remove(category);
    await _context.SaveChangesAsync();

    return true;
}
}