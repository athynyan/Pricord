using MediatR;
using Pricord.Application.Timelines.Contracts.Dtos;
using Pricord.Application.Timelines.Persistence;

namespace Pricord.Application.Timelines.Queries.GetTimeline;

public sealed class GetTimelineQueryHandler : IRequestHandler<GetTimelineQuery, TimelineDto?>
{
    private readonly ITimelineRepository _repository;

    public GetTimelineQueryHandler(ITimelineRepository repository)
    {
        _repository = repository;
    }

    public async Task<TimelineDto?> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(request.Id);
    }
}
