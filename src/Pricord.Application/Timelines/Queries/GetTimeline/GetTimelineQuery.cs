using MediatR;
using Pricord.Application.Timelines.Contracts.Dtos;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Application.Timelines.Queries.GetTimeline;

public sealed record GetTimelineQuery(TimelineId Id) : IRequest<TimelineDto?>;