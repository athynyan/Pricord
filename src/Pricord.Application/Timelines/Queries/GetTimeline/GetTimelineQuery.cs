using MediatR;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Application.Timelines.Queries.GetTimeline;

public sealed record GetTimelineQuery(TimelineId Id) : IRequest<Timeline?>;