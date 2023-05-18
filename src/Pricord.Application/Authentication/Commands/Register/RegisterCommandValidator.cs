using FluentValidation;
using Pricord.Domain.Authentication.Enums;

namespace Pricord.Application.Authentication.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.Name.Value)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(32)
            .Matches(@"^[a-zA-Z0-9_]+$")
            .WithMessage("Name must be between 3 and 32 characters and contain only letters, numbers, and underscores.");

        RuleFor(c => c.Email!.Value)
            .EmailAddress().When(c => c.Email is not null)
            .WithMessage("Provided email has invalid format.");
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,64}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.");

        RuleFor(c => c.Role)
            .IsEnumName(typeof(Role))
            .WithMessage("Provided role is invalid.");
    }
}