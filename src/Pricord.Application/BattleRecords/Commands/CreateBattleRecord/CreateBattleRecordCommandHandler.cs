using MediatR;
using Pricord.Application.BattleRecords.Models;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Timelines.Models;
using Pricord.Application.Timelines.Persistence;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.Enums;
using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.Enums;
using Pricord.Domain.Timelines.ValueObjects;
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

        var timeline = CreateTimelineModel(request.Timeline);

        if (timeline is not null) 
        {
            battleRecord = BattleRecord.Create(
                request.Boss.Id,
                request.PlayableCharacters,
                expectedDamage,
                timeline.Id);

            await _timelineRepository.AddAsync(timeline);
        }
        else 
        {
            battleRecord = BattleRecord.Create(
                request.Boss.Id,
                request.PlayableCharacters,
                expectedDamage
                );
        }

        await Task.WhenAll(
            _bossRepository.AddAsync(request.Boss),
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

    private Timeline? CreateTimelineModel(CreateTimelineModel? timeline)
    {
        if (timeline is null) return null;

        var items = timeline!.Items
            .Select(i => TimelineItem.Create(
                i.Time, 
                PrefabId.Create(i.AttackerId), 
                Enum.Parse<ActionType>(i.ActionType, true), 
                i.AdditionalInfo))
            .ToArray();

        var video = timeline!.Video is not null
            ? Video.Create(timeline!.Video.Url, Enum.Parse<VideoType>(timeline!.Video.Type, true))
            : null;

        return video is null 
            ? Timeline.Create(items) 
            : Timeline.Create(items, video);
    }
}
