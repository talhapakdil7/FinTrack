using FinTrack.API.DTOs;
using FinTrack.API.Entities;
using FinTrack.API.Interfaces;
using FinTrack.API.Data;

namespace FinTrack.API.Services;

public class TransactionService : ITransactionService
{
    // DbContext inject ediyoruz
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<TransactionDto> GetAll()
    {
        return _context.Transactions.Select(t => new TransactionDto
        {
            Id = t.Id,
            Title = t.Title,
            Amount = t.Amount,
            Type = t.Type,
            TransactionDate = t.TransactionDate,
            Note = t.Note,
            CreatedAt = t.CreatedAt
        });
    }

    public TransactionDto Create(CreateTransactionDto dto)
    {
        var transaction = new Transaction
        {
            Title = dto.Title,
            Amount = dto.Amount,
            Type = dto.Type,
            TransactionDate = dto.TransactionDate,
            Note = dto.Note,
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        _context.SaveChanges();

        return new TransactionDto
        {
            Id = transaction.Id,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Type = transaction.Type,
            TransactionDate = transaction.TransactionDate,
            Note = transaction.Note,
            CreatedAt = transaction.CreatedAt
        };
    }

    public bool Delete(int id)
    {
        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null)
            return false;

        _context.Transactions.Remove(transaction);
        _context.SaveChanges();

        return true;
    }

    public TransactionDto? Update(int id, CreateTransactionDto dto)
    {
        var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null)
            return null;

        transaction.Title = dto.Title;
        transaction.Amount = dto.Amount;
        transaction.Type = dto.Type;
        transaction.TransactionDate = dto.TransactionDate;
        transaction.Note = dto.Note;
        transaction.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();

        return new TransactionDto
        {
            Id = transaction.Id,
            Title = transaction.Title,
            Amount = transaction.Amount,
            Type = transaction.Type,
            TransactionDate = transaction.TransactionDate,
            Note = transaction.Note,
            CreatedAt = transaction.CreatedAt
        };
    }
}