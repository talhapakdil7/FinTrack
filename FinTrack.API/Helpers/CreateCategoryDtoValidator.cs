using FluentValidation;
using FinTrack.API.DTOs;

namespace FinTrack.API.Helpers;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Kategori adı boş olamaz");

        RuleFor(x => x.Type)
            .Must(t => t == "Income" || t == "Expense")
            .WithMessage("Type sadece Income veya Expense olabilir");
    }
}