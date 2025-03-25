using Application.Features.Depreciations.Commands.Create;
using Application.Features.Depreciations.Queries.GetAll;
using Application.Features.Depreciations.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepreciationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepreciationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Depreciation>>> GetAll()
    {
        var depreciaations = await _mediator.Send(new GetAllDepreciationsQuery());
        return Ok(depreciaations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Depreciation>> GetById(Guid id)
    {
        var depreciation = await _mediator.Send(new GetDepreciationByIdQuery(id));
        return depreciation == null ? NotFound() : Ok(depreciation);
    }

    [HttpPost]
    public async Task<ActionResult<Depreciation>> Create([FromBody] CreateDepreciationCommand command)
    {
        var depreciation = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = depreciation.Id }, depreciation);
    }
}
