using FluentValidation;
using TravelCompanion.Modules.Travels.Core.DTO;

namespace TravelCompanion.Modules.Travels.Core.Validators;

internal sealed class TravelDtoValidator : AbstractValidator<TravelUpsertDto>
{
    public TravelDtoValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(3)
            .MaximumLength(25)
            .WithMessage("Title's length should be between 3 and 25 chars.");

        RuleFor(x => x.Description)
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Title's length should be between 3 and 100 chars.");
    }
}