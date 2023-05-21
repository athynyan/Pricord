using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pricord.Application.Timelines.Queries.GetTimeline;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Api.Timelines;

[ApiController]
[Route("api/[controller]")]
public sealed class TimelinesController : ControllerBase
{
    private readonly ISender _sender;

    public TimelinesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeline([FromRoute] Guid id)
    {
        var result = await _sender.Send(new GetTimelineQuery(TimelineId.Create(id)));
        return result is null ? NotFound() : Ok(result.ToDto());
    }
}