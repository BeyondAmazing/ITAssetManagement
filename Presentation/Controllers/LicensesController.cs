using Application.Features.Licenses.Commands.Create;
using Application.Features.Licenses.Queries.ById;
using Application.Features.Licenses.Queries.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class LicensesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicensesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<License>>> GetAll()
    {
        var licenses = await _mediator.Send(new GetAllLicensesQuery());
        return Ok(licenses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<License>> GetById(Guid id)
    {
        var license = await _mediator.Send(new GetLicenseByIdQuery(id));
        return license == null ? NotFound() : Ok(license);
    }

    [HttpPost]
    public async Task<ActionResult<License>> Create([FromBody] CreateLicenseCommand command)
    {
        var license = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = license.Id }, license);
    }
}
