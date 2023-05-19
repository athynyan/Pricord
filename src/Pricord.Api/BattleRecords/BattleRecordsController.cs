using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pricord.Api.BattleRecords.Contracts;
using Pricord.Api.BattleRecords.Mappers;
using Pricord.Application.BattleRecords.Queries.GetAllBattleRecords;
using Pricord.Application.BattleRecords.Queries.GetBattleRecord;
using Pricord.Domain.BattleRecords.ValueObjects;

namespace Pricord.Api.BattleRecords;

[ApiController]
[Route("api/[controller]")]
public sealed class BattleRecordsController : ControllerBase
{
    private readonly ISender _sender;

    public BattleRecordsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody]CreateBattleRecordRequest request)
    {
        var result = await _sender.Send(request.ToCommand());
        return Ok(result.ToResponse());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _sender.Send(new GetBattleRecordDetailsQuery(BattleRecordId.Create(id)));
        return Ok(result.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllBattleRecordsQuery());
        return Ok(result.Select(r => r.ToResponse()));
    }
}