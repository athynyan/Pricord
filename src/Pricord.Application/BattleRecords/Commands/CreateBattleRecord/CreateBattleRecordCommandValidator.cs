using FluentValidation;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.BattleRecords.Validators;
using Pricord.Application.Timelines.Validators;
using Pricord.Domain.BattleRecords.Enums;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Application.BattleRecords.Commands.CreateBattleRecord;

public sealed class CreateBattleRecordCommandValidator : AbstractValidator<CreateBattleRecordCommand>
{
    private readonly IUnitRepository _unitRepository;

    private List<PrefabId> _playableCharacterPrefabIds = new();
    
    public CreateBattleRecordCommandValidator(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;

        RuleFor(br => br.ExpectedDamage)
            .GreaterThan(0)
            .WithMessage("Expected damage must be greater than 0.");

        RuleFor(br => br.BattleType)
            .Must(bt => Enum.TryParse(bt, true, out BattleType _))
            .WithMessage("Battle type must be one of the following: " + string.Join(", ", Enum.GetNames<BattleType>()));

        RuleFor(br => br.Boss)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Boss must be specified.")
            .SetValidator(new BossDtoValidator(ValueMustExist));
        
        RuleFor(br => br.PlayableCharacters)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("At least one playable character must be specified.")
            .ForEach(pc => pc
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Playable character must be specified.")
                .SetValidator(new PlayableCharacterDtoValidator(ValueMustExist)));
        
        RuleFor(br => br.Timeline)
            .SetValidator(new TimelineDtoValidator(_playableCharacterPrefabIds)!)
            .When(br => br.Timeline is not null);
    }

    private async Task<bool> ValueMustExist(string prefabIdString, CancellationToken token)
    {
        var prefabId = PrefabId.Create(prefabIdString);

        if (!_playableCharacterPrefabIds.Contains(prefabId) 
            && _playableCharacterPrefabIds.Count < 5
            && prefabId.Value.StartsWith("1")
            && _unitRepository.ExistsAsync(prefabId).Result)
        {
            _playableCharacterPrefabIds.Add(prefabId);
            return true;
        }

        if (_playableCharacterPrefabIds.Contains(prefabId))
        {
            return true;
        }

        return await _unitRepository.ExistsAsync(prefabId);
    }
}
