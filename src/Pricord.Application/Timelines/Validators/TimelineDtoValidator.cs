using FluentValidation;
using FluentValidation.Validators;
using Pricord.Application.BattleRecords.Commands.CreateBattleRecord;
using Pricord.Application.Timelines.Contracts.Dtos;
using Pricord.Domain.Timelines.Enums;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Application.Timelines.Validators;

internal class TimelineDtoValidator : IPropertyValidator<CreateBattleRecordCommand, TimelineDto>
{
    private string _errorMessage = string.Empty;
    private readonly IEnumerable<PrefabId> _playableCharacterPrefabIds = default!;

    public TimelineDtoValidator(IEnumerable<PrefabId> playableCharacterPrefabIds)
    {
        _playableCharacterPrefabIds = playableCharacterPrefabIds;
    }

    public string Name => "TimelineDtoValidator";

    public string GetDefaultMessageTemplate(string errorCode)
    {  
        return _errorMessage;
    }

    public bool IsValid(ValidationContext<CreateBattleRecordCommand> context, TimelineDto value)
    {
        var timelineDto = context.InstanceToValidate.Timeline!;
        
        if (timelineDto.Items is null || timelineDto.Items.Count() == 0) 
            return false;

        if (timelineDto.Video is not null)
        {
            if (!Enum.TryParse(timelineDto.Video.Type, true, out VideoType _))
            {
                _errorMessage = "Timeline video type must be one of the following: " + string.Join(", ", Enum.GetNames<VideoType>());
                return false;
            }

            if (timelineDto.Video.Url is null)
            {
                _errorMessage = "Timeline video url must be specified.";
                return false;
            }
        }

        foreach (var item in timelineDto.Items)
        {
            if (item.Time <= 0)
            {
                _errorMessage = "Timeline item time must be greater than 0.";
                return false;
            }
            
            if (item.Time > 90)
            {
                _errorMessage = "Timeline item time must be less than or equal to 90.";
                return false;
            }
            
            if (!Enum.TryParse(item.ActionType, true, out ActionType _))
            {
                _errorMessage = "Timeline item action type must be one of the following: " + string.Join(", ", Enum.GetNames<ActionType>());
                return false;
            }

            if (!_playableCharacterPrefabIds.Contains(PrefabId.Create(item.AttackerId)))
            {
                _errorMessage = "Timeline item attacker id must be one of the following: " + string.Join(", ", _playableCharacterPrefabIds.Select(pc => pc.Value));
                return false;
            }
        }

        return true;
    }
}
