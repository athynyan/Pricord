using FluentValidation;
using Pricord.Application.BattleRecords.Contracts.Dtos;

namespace Pricord.Application.BattleRecords.Commands.CreateBattleRecord;

internal class BossDtoValidator : AbstractValidator<BossDto>
{
    public BossDtoValidator(Func<string, CancellationToken, Task<bool>> valueMustExist)
    {
        RuleFor(b => b.PrefabId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss prefab id must be specified.")
            .Matches("^3\\d{5}$")
            .WithMessage("Boss prefab id must be a 6 digit number starting with 3.")
            .MustAsync(valueMustExist)
            .WithMessage("Boss must exist.");
        
        RuleFor(b => b.Health)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss health must be specified.");
        
        RuleFor(b => b.Level)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss level must be specified.");
        
        RuleFor(b => b.Tier)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss tier must be specified.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Boss tier must be greater than or equal to 1.")
            .LessThanOrEqualTo(4)
            .WithMessage("Boss tier must be less than or equal to 4.");
    }
}
