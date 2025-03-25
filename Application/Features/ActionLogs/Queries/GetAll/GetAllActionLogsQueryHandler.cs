using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ActionLogs.Queries.GetAll;
public class GetAllActionLogsQueryHandler : IRequestHandler<GetAllActionLogsQuery, IEnumerable<ActionLog>>
{
    private readonly IActionLogRepository _actionLogRepository;

    public GetAllActionLogsQueryHandler(IActionLogRepository actionLogRepository)
    {
        _actionLogRepository = actionLogRepository;
    }

    public async Task<IEnumerable<ActionLog>> Handle(GetAllActionLogsQuery request, CancellationToken cancellationToken)
    {
        return await _actionLogRepository.GetAllAsync();
    }
}
