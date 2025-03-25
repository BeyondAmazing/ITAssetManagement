using Application.Features.Manufacturers.Commands.Create;
using Application.Features.Manufacturers.Queries.GetAll;
using Application.Features.Manufacturers.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManufacturersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetAll()
        {
            var manufacturers = await _mediator.Send(new GetAllManufacturersQuery());
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetById(Guid id)
        {
            var manufacturer = await _mediator.Send(new GetManufacturerByIdQuery(id));
            return manufacturer == null ? NotFound() : Ok(manufacturer);
        }

        [HttpPost]
        public async Task<ActionResult<Manufacturer>> Create([FromBody] CreateManufacturerCommand command)
        {
            var manufacturer = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = manufacturer.Id }, manufacturer);
        }
    }
}
