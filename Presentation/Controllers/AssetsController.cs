using Application.Features.Assets.Commands.CheckInOut;
using Application.Features.Assets.Commands.Create;
using Application.Features.Assets.Commands.Delete;
using Application.Features.Assets.Commands.Update;
using Application.Features.Assets.Queries.ById;
using Application.Features.Assets.Queries.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AssetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Asset>>> GetAll()
    {
        var assets = await _mediator.Send(new GetAllAssetsQuery());
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Asset>> GetById(Guid id)
    {
        var asset = await _mediator.Send(new GetAssetByIdQuery(id));
        return asset == null ? NotFound() : Ok(asset);
    }

    [HttpPost]
    public async Task<ActionResult<Asset>> Create([FromBody] CreateAssetCommand command)
    {
        var asset = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = asset.Id }, asset);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Asset>> Update(Guid id, [FromBody] UpdateAssetCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var asset = await _mediator.Send(command);
        return Ok(asset);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAssetCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/checkout")]
    public async Task<IActionResult> CheckOut(Guid id, [FromBody] CheckOutAssetCommand command)
    {
        if (id != command.AssetId) 
            return BadRequest("ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/checkin")]
    public async Task<IActionResult> CheckIn(Guid id)
    {
        await _mediator.Send(new CheckInAssetCommand(id));
        return NoContent();
    }
}
