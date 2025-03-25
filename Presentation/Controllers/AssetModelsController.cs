using Application.Features.AssetModels.Commands.Create;
using Application.Features.AssetModels.Queries.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AssetModelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetModelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetModel>>> GetAll()
    {
        var models = await _mediator.Send(new GetAllAssetModelsQuery());
        return Ok(models);
    }

    [HttpPost]
    public async Task<ActionResult<AssetModel>> Create([FromBody] CreateAssetModelCommand command)
    {
        var model = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), null, model); // No specific GetById yet
    }
}
