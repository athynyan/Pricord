using MediatR;
using Pricord.Application.Timelines.Persistence;
using Pricord.Domain.Timelines;

namespace Pricord.Application.Timelines.Queries.GetTimeline;

public sealed class GetTimelineQueryHandler : IRequestHandler<GetTimelineQuery, Timeline?>
{
    private readonly ITimelineRepository _repository;

    public GetTimelineQueryHandler(ITimelineRepository repository)
    {
        _repository = repository;
    }

    public async Task<Timeline?> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(request.Id);
    }
}
