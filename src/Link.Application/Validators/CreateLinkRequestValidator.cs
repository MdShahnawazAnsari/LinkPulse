using FluentValidation;
using Link.Application.DTOs;

namespace Link.Application.Validators;

public class CreateLinkRequestValidator : AbstractValidator<CreateLinkRequest>
{
    public CreateLinkRequestValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty().WithMessage("OriginalUrl is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Please enter a valid URL (e.g., https://google.com).");

        RuleFor(x => x.ExpiryDays)
            .GreaterThan(0).When(x => x.ExpiryDays.HasValue)
            .WithMessage("Expiry days must be positive.");
    }
}