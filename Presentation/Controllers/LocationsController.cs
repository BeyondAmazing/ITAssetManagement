using Application.Features.Locations.Commands.Create;
using Application.Features.Locations.Queries.GetAll;
using Application.Features.Locations.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll()
        {
            var locations = await _mediator.Send(new GetAllLocationsQuery());
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetById(Guid id)
        {
            var location = await _mediator.Send(new GetLocationByIdQuery(id));
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public async Task<ActionResult<Location>> Create([FromBody] CreateLocationCommand command)
        {
            var location = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = location.Id }, location);
        }
    }
}
