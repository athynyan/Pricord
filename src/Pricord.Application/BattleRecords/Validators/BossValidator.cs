using FluentValidation;
using Pricord.Domain.Units;

namespace Pricord.Application.BattleRecords.Commands.CreateBattleRecord;

internal class BossValidator : AbstractValidator<Boss>
{
    public BossValidator(Func<string, CancellationToken, Task<bool>> valueMustExist)
    {
        RuleFor(b => b.PrefabId.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss prefab id must be specified.")
            .Matches("^3\\d{5}$")
            .WithMessage("Boss prefab id must be a 6 digit number starting with 3.")
            .MustAsync(valueMustExist)
            .WithMessage("Boss must exist.");
        
        RuleFor(b => b.Health.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss health must be specified.");
        
        RuleFor(b => b.Level.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss level must be specified.");
        
        RuleFor(b => (int)b.Tier)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss tier must be specified.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Boss tier must be greater than or equal to 1.")
            .LessThanOrEqualTo(4)
            .WithMessage("Boss tier must be less than or equal to 4.");
    }
}
