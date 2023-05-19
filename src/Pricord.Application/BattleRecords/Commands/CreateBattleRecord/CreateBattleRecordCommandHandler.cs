using MediatR;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Timelines.Contracts.Dtos;
using Pricord.Application.Timelines.Persistence;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.Enums;
using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.Enums;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units;
using Pricord.Domain.Units.Enums;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Application.BattleRecords.Commands.CreateBattleRecord;

public sealed class CreateBattleRecordCommandHandler : IRequestHandler<CreateBattleRecordCommand, BattleRecordResult>
{
    private readonly IBattleRecordRepository _battleRecordRepository;
    private readonly IBossRepository _bossRepository;
    private readonly ITimelineRepository _timelineRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBattleRecordCommandHandler(
        IBattleRecordRepository battleRecordRepository,
        IBossRepository bossRepository,
        ITimelineRepository timelineRepository,
        IUnitOfWork unitOfWork)
    {
        _battleRecordRepository = battleRecordRepository;
        _bossRepository = bossRepository;
        _timelineRepository = timelineRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BattleRecordResult> Handle(CreateBattleRecordCommand request, CancellationToken cancellationToken)
    {   
        BattleRecord battleRecord;

        var expectedDamage = Damage.Create(request.ExpectedDamage, Enum.Parse<BattleType>(request.BattleType, true));

        var timeline = CreateTimeline(request.Timeline);
        var boss = CreateBoss(request.Boss);
        var playableCharacters = CreatePlayableCharacters(request.PlayableCharacters);

        if (timeline is not null) 
        {
            battleRecord = BattleRecord.Create(
                boss.Id,
                playableCharacters,
                expectedDamage,
                timeline.Id);

            await _timelineRepository.AddAsync(timeline);
        }
        else 
        {
            battleRecord = BattleRecord.Create(
                boss.Id,
                playableCharacters,
                expectedDamage
                );
        }

        await Task.WhenAll(
            _bossRepository.AddAsync(boss),
            _battleRecordRepository.AddAsync(battleRecord));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new BattleRecordResult(
            battleRecord.Id,
            battleRecord.BossId,
            battleRecord.ExpectedDamage,
            battleRecord.TimelineId,
            battleRecord.PlayableCharacters
                .Select(pc => pc.Id)
                .ToArray());
    }

    private PlayableCharacter[] CreatePlayableCharacters(PlayableCharacterDto[] playableCharacters)
    {
        return playableCharacters
            .Select(pc => PlayableCharacter.Create(
                PrefabId.Create(pc.PrefabId),
                Level.Create(pc.Level),
                Rank.Create(pc.Rank),
                Rarity.Create(pc.Rarity)))
            .ToArray();
    }

    private Boss CreateBoss(BossDto boss)
    {
        return Boss.Create(
            PrefabId.Create(boss.PrefabId),
            Level.Create(boss.Level),
            Health.Create(boss.Health),
            (Tier) boss.Tier
        );
    }

    private Timeline? CreateTimeline(TimelineDto? timelineDto)
    {   
        if (timelineDto is null) return null;

        var items = timelineDto!.Items
            .Select(i => TimelineItem.Create(
                i.Time, 
                PrefabId.Create(i.AttackerId), 
                Enum.Parse<ActionType>(i.ActionType, true), 
                i.AdditionalInfo))
            .ToArray();

        var video = timelineDto!.Video is not null
            ? Video.Create(timelineDto!.Video.Url, Enum.Parse<VideoType>(timelineDto!.Video.Type, true))
            : null;

        return video is null 
            ? Timeline.Create(items) 
            : Timeline.Create(items, video);
    }
}
