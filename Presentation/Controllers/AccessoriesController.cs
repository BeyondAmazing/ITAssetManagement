using Application.Features.Accessories.Commands.CheckOut;
using Application.Features.Accessories.Commands.Create;
using Application.Features.Accessories.Queries.GetAll;
using Application.Features.Accessories.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccessoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accessory>>> GetAll()
        {
            var accessories = await _mediator.Send(new GetAllAccessoriesQuery());
            return Ok(accessories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Accessory>> GetById(Guid id)
        {
            var accessory = await _mediator.Send(new GetAccessoryByIdQuery(id));
            return accessory == null ? NotFound() : Ok(accessory);
        }

        [HttpPost]
        public async Task<ActionResult<Accessory>> Create([FromBody] CreateAccessoryCommand command)
        {
            var accessory = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), null, accessory);
        }

        [HttpPost("{id}/checkout")]
        public async Task<IActionResult> CheckOut(Guid id, [FromBody] CheckOutAccessoryCommand command)
        {
            if (id != command.AccessoryId) 
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
