using Application.Features.Consumables.Commands.CheckOut;
using Application.Features.Consumables.Commands.Create;
using Application.Features.Consumables.Queries.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConsumablesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsumablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumable>>> GetAll()
        {
            var consumables = await _mediator.Send(new GetAllConsumablesQuery());
            return Ok(consumables);
        }

        [HttpPost]
        public async Task<ActionResult<Consumable>> Create([FromBody] CreateConsumableCommand command)
        {
            var consumable = await _mediator.Send(command);
            return consumable == null ? NoContent() : Ok(consumable);
        }

        [HttpPost("{id}/checkout")]
        public async Task<ActionResult> Checkout(Guid id, [FromBody] CheckOutConsumableCommand command)
        {
            if (id != command.ConsumableId)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
