using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ActionLogs.Queries.GetById;
public class GetActionLogByIdQueryHandler : IRequestHandler<GetActionLogByIdQuery, ActionLog?>
{
    private readonly IActionLogRepository _actionLogRepository;

    public GetActionLogByIdQueryHandler(IActionLogRepository actionLogRepository)
    {
        _actionLogRepository = actionLogRepository;
    }

    public async Task<ActionLog?> Handle(GetActionLogByIdQuery request, CancellationToken cancellationToken)
    {
        return await _actionLogRepository.GetByIdAsync(request.Id);
    }
}
