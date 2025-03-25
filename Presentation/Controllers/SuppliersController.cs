using Application.Features.Suppliers.Commands.Create;
using Application.Features.Suppliers.Queries.GetAll;
using Application.Features.Suppliers.Queries.GetById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
        {
            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetById(Guid id)
        {
            var supplier = await _mediator.Send(new GetSupplierByIdQuery(id));
            return supplier == null ? NotFound() : Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> Create([FromBody] CreateSupplierCommand command)
        {
            var supplier = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }
    }
}
