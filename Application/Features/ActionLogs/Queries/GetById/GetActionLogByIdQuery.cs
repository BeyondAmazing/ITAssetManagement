using Domain.Entities;
using MediatR;

namespace Application.Features.ActionLogs.Queries.GetById;

public record GetActionLogByIdQuery(Guid Id) : IRequest<ActionLog?> { }
