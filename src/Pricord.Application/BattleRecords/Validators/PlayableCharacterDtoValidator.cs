using FluentValidation;
using Pricord.Application.BattleRecords.Contracts.Dtos;

namespace Pricord.Application.BattleRecords.Validators
{
    internal class PlayableCharacterDtoValidator : AbstractValidator<PlayableCharacterDto>
    {
        public PlayableCharacterDtoValidator(Func<string, CancellationToken, Task<bool>> valueMustExist)
        {
            RuleFor(pcc => pcc.PrefabId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Playable character prefab id must be specified.")
                .Matches("^1\\d{5}$")
                .WithMessage("Playable character prefab id must be a 6 digit number starting with 1.")
                .MustAsync(valueMustExist)
                .WithMessage("Playable character must exist.");
            
            RuleFor(pcc => pcc.Level)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Playable character level must be specified.");
            
            RuleFor(pcc => pcc.Rarity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Playable character rarity must be specified.")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Playable character rarity must be greater than or equal to 1.")
                .LessThanOrEqualTo(6)
                .WithMessage("Playable character tier must be less than or equal to 6.");
            
            RuleFor(pcc => pcc.Rank)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Playable character rank must be specified.");
        }
    }
}