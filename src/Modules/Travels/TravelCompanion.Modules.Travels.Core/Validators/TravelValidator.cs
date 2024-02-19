using FluentValidation;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.Validators;

public class TravelValidator : AbstractValidator<Travel>
{
    public TravelValidator()
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