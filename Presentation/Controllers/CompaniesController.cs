using Application.Features.Companies.Commands.Create;
using Application.Features.Companies.Queries.GetAll;
using Application.Features.Companies.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetAll()
    {
        var companies = await _mediator.Send(new GetAllCompaniesQuery());
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetById(Guid id)
    {
        var company = await _mediator.Send(new GetCompanyByIdQuery(id));
        return company == null ? NotFound() : Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult<Company>> Create([FromBody] CreateCompanyCommand command)
    {
        var company = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
    }
}
