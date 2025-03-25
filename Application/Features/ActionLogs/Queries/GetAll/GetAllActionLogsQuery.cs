using Domain.Entities;
using MediatR;

namespace Application.Features.ActionLogs.Queries.GetAll;

public record GetAllActionLogsQuery : IRequest<IEnumerable<ActionLog>> { }
