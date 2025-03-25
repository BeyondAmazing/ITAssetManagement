using Domain.Entities;
using MediatR;

namespace Application.Features.ActionLogs.Queries.GetByItemQuery;

public record GetActionLogsByItemQuery(string ItemType, Guid ItemId) : IRequest<IEnumerable<ActionLog>> { }
