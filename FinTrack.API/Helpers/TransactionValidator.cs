using FluentValidation;
using FinTrack.API.DTOs;

namespace FinTrack.API.Helpers;

public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionDtoValidator()
    {
        // Title boş olamaz
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title boş olamaz");

        // Amount 0'dan büyük olmalı
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount 0'dan büyük olmalı");

        // Type kontrolü
        RuleFor(x => x.Type)
            .Must(t => t == "Income" || t == "Expense")
            .WithMessage("Type sadece Income veya Expense olabilir");
    }
}