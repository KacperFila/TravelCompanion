using FluentValidation;
using TravelCompanion.Modules.Travels.Core.Dto;

namespace TravelCompanion.Modules.Travels.Core.Validators;

internal sealed class TravelDtoValidator : AbstractValidator<TravelDto>
{
    public TravelDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(25)
            .WithMessage("Incorrect data");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Incorrect data");
    }
}