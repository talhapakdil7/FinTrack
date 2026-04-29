using Microsoft.EntityFrameworkCore;
using FinTrack.API.Data;
using FinTrack.API.DTOs;
using FinTrack.API.Interfaces;
using Google.GenAI;

namespace FinTrack.API.Services;

public class AiService : IAiService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AiService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AiInsightDto> GenerateFinancialInsightAsync(int userId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.TransactionDate)
            .Take(20)
            .ToListAsync();

        var totalIncome = transactions
            .Where(t => t.Type == "Income")
            .Sum(t => t.Amount);

        var totalExpense = transactions
            .Where(t => t.Type == "Expense")
            .Sum(t => t.Amount);

        var prompt = $"""
        You are a financial assistant.

        Analyze this data and give a short insight.

        Income: {totalIncome}
        Expense: {totalExpense}
        Balance: {totalIncome - totalExpense}

        Keep it short and helpful.
        """;

        var apiKey = _configuration["Gemini:ApiKey"];
        var model = _configuration["Gemini:Model"] ?? "gemini-3-flash-preview";

        var client = new Client(apiKey: apiKey);

        var response = await client.Models.GenerateContentAsync(
            model: model,
            contents: prompt
        );

        var text = response.Candidates[0].Content.Parts[0].Text;

        return new AiInsightDto
        {
            Insight = text ?? "No insight generated"
        };
    }
}