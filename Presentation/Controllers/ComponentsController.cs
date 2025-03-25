using Application.Features.Components.Commands.Assign;
using Application.Features.Components.Commands.Create;
using Application.Features.Components.Queries.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ComponentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComponentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Component>>> GetAll()
    {
        var components = await _mediator.Send(new GetAllComponentsQuery());
        return Ok(components);
    }

    [HttpPost]
    public async Task<ActionResult<Component>> Create([FromBody] CreateComponentCommand command)
    {
        var component = await _mediator.Send(command);
        return Ok(component);
    }

    [HttpPost("{id}/assign")]
    public async Task<ActionResult> AssignComponent(Guid id, [FromBody] AssignComponentCommand command)
    {
        if (id != command.ComponentId)
            return BadRequest("ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }
}
