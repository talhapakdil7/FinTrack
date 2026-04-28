using Microsoft.EntityFrameworkCore;
using FinTrack.API.DTOs;
using FinTrack.API.Entities;
using FinTrack.API.Interfaces;
using FinTrack.API.Data;

namespace FinTrack.API.Services;

public class TransactionService : ITransactionService
{
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllAsync(int userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                CategoryId = t.CategoryId,
                CategoryName = t.Category != null ? t.Category.Name : null,
                Title = t.Title,
                Amount = t.Amount,
                Type = t.Type,
                TransactionDate = t.TransactionDate,
                Note = t.Note,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto, int userId)
    {

        if (dto.CategoryId != null)
{
    var categoryExists = await _context.Categories
        .AnyAsync(c => c.Id == dto.CategoryId && c.UserId == userId);

    if (!categoryExists)
        throw new Exception("Geçersiz kategori");
}

        var transaction = new Transaction
        {
            Title = dto.Title,
            Amount = dto.Amount,
            Type = dto.Type,
            TransactionDate = dto.TransactionDate,
            Note = dto.Note,
            CategoryId = dto.CategoryId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();

        var categoryName = await _context.Categories
            .Where(c => c.Id == transaction.CategoryId && c.UserId == userId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        return new TransactionDto
        {
            Id = transaction.Id,
            CategoryId = transaction.CategoryId,
            CategoryName = categoryName,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Type = transaction.Type,
            TransactionDate = transaction.TransactionDate,
            Note = transaction.Note,
            CreatedAt = transaction.CreatedAt
        };
    }

    public async Task<TransactionDto?> UpdateAsync(int id, CreateTransactionDto dto, int userId)
    {

        if (dto.CategoryId != null)
{
    var categoryExists = await _context.Categories
        .AnyAsync(c => c.Id == dto.CategoryId && c.UserId == userId);

    if (!categoryExists)
        throw new Exception("Geçersiz kategori");
}

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (transaction == null)
            return null;

        transaction.Title = dto.Title;
        transaction.Amount = dto.Amount;
        transaction.Type = dto.Type;
        transaction.TransactionDate = dto.TransactionDate;
        transaction.Note = dto.Note;
        transaction.CategoryId = dto.CategoryId;
        transaction.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var categoryName = await _context.Categories
            .Where(c => c.Id == transaction.CategoryId && c.UserId == userId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        return new TransactionDto
        {
            Id = transaction.Id,
            CategoryId = transaction.CategoryId,
            CategoryName = categoryName,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Type = transaction.Type,
            TransactionDate = transaction.TransactionDate,
            Note = transaction.Note,
            CreatedAt = transaction.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (transaction == null)
            return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return true;
    }
}