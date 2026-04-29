using Microsoft.EntityFrameworkCore;
using FinTrack.API.Data;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;

namespace FinTrack.API.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> GetSummaryAsync(int userId)
    {
        var totalIncome = await _context.Transactions
            .Where(t => t.UserId == userId && t.Type == "Income")
            .SumAsync(t => t.Amount);

        var totalExpense = await _context.Transactions
            .Where(t => t.UserId == userId && t.Type == "Expense")
            .SumAsync(t => t.Amount);

        return new ReportDto
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = totalIncome - totalExpense
        };
    }
    public async Task<IEnumerable<CategoryReportDto>> GetCategorySummaryAsync(int userId)
{
    return await _context.Transactions
        // Sadece giriş yapan kullanıcının giderlerini alıyoruz
        .Where(t => t.UserId == userId && t.Type == "Expense")

        // CategoryId ve CategoryName'e göre grupluyoruz
        .GroupBy(t => new
        {
            t.CategoryId,
            CategoryName = t.Category != null ? t.Category.Name : "Uncategorized"
        })

        // Her kategori için toplam harcamayı hesaplıyoruz
        .Select(g => new CategoryReportDto
        {
            CategoryId = g.Key.CategoryId,
            CategoryName = g.Key.CategoryName,
            TotalAmount = g.Sum(t => t.Amount)
        })

        .ToListAsync();
}
}