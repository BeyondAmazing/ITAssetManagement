using Application.Features.ActionLogs.Queries.GetAll;
using Application.Features.ActionLogs.Queries.GetById;
using Application.Features.ActionLogs.Queries.GetByItemQuery;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActionLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActionLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionLog>>> GetAll()
        {
            var actionLogs = await _mediator.Send(new GetAllActionLogsQuery());
            return Ok(actionLogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionLog>> GetById(Guid id)
        {
            var actionLog = await _mediator.Send(new GetActionLogByIdQuery(id));
            return actionLog == null ? NotFound() : Ok(actionLog);
        }

        [HttpGet("{itemType}/{itemId}")]
        public async Task<ActionResult<ActionLog>> GetByItem(string itemType, Guid itemId)
        {
            var actionLog = await _mediator.Send(new GetActionLogsByItemQuery(itemType, itemId));
            return actionLog == null ? NotFound() : Ok(actionLog);
        }
    }
}
