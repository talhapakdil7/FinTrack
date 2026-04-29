using Microsoft.EntityFrameworkCore;
using FinTrack.API.Data;
using FinTrack.API.DTOs;
using FinTrack.API.Entities;
using FinTrack.API.Interfaces;

namespace FinTrack.API.Services;

public class BudgetService : IBudgetService
{
    private readonly AppDbContext _context;

    public BudgetService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BudgetDto> CreateAsync(CreateBudgetDto dto, int userId)
    {
        var existingBudget = await _context.Budgets
            .FirstOrDefaultAsync(b =>
                b.UserId == userId &&
                b.Month == dto.Month &&
                b.Year == dto.Year);

        if (existingBudget != null)
        {
            existingBudget.LimitAmount = dto.LimitAmount;
            await _context.SaveChangesAsync();

            return await BuildBudgetDto(existingBudget);
        }

        var budget = new Budget
        {
            Month = dto.Month,
            Year = dto.Year,
            LimitAmount = dto.LimitAmount,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Budgets.AddAsync(budget);
        await _context.SaveChangesAsync();

        return await BuildBudgetDto(budget);
    }

    public async Task<BudgetDto?> GetCurrentAsync(int userId)
    {
        var now = DateTime.UtcNow;

        var budget = await _context.Budgets
            .FirstOrDefaultAsync(b =>
                b.UserId == userId &&
                b.Month == now.Month &&
                b.Year == now.Year);

        if (budget == null)
            return null;

        return await BuildBudgetDto(budget);
    }

    private async Task<BudgetDto> BuildBudgetDto(Budget budget)
    {
        var totalExpense = await _context.Transactions
            .Where(t =>
                t.UserId == budget.UserId &&
                t.Type == "Expense" &&
                t.TransactionDate.Month == budget.Month &&
                t.TransactionDate.Year == budget.Year)
            .SumAsync(t => t.Amount);

        var remainingAmount = budget.LimitAmount - totalExpense;

        var usagePercentage = budget.LimitAmount == 0
            ? 0
            : (totalExpense / budget.LimitAmount) * 100;

        return new BudgetDto
        {
            Id = budget.Id,
            Month = budget.Month,
            Year = budget.Year,
            LimitAmount = budget.LimitAmount,
            TotalExpense = totalExpense,
            RemainingAmount = remainingAmount,
            UsagePercentage = usagePercentage
        };
    }
}