using Application.Features.StatusLabels.Commands.Create;
using Application.Features.StatusLabels.Queries.GetAll;
using Application.Features.StatusLabels.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StatusLabelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatusLabelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatusLabel>>> GetAll()
    {
        var statusLabels = await _mediator.Send(new GetAllStatusLabelsQuery());
        return Ok(statusLabels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StatusLabel>> GetById(Guid id)
    {
        var statusLabel = await _mediator.Send(new GetStatusLabelByIdQuery(id));
        if (statusLabel == null) return NotFound();
        return Ok(statusLabel);
    }

    [HttpPost]
    public async Task<ActionResult<StatusLabel>> Create([FromBody] CreateStatusLabelCommand command)
    {
        var statusLabel = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = statusLabel.Id }, statusLabel);
    }
}
