using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ActionLogs.Queries.GetByItemQuery;
public class GetActionLogsByItemQueryHandler : IRequestHandler<GetActionLogsByItemQuery, IEnumerable<ActionLog>>
{
    private readonly IActionLogRepository _actionLogRepository;

    public GetActionLogsByItemQueryHandler(IActionLogRepository actionLogRepository)
    {
        _actionLogRepository = actionLogRepository;
    }

    public async Task<IEnumerable<ActionLog>> Handle(GetActionLogsByItemQuery request, CancellationToken cancellationToken)
    {
        return await _actionLogRepository.GetByItemAsync(request.ItemType, request.ItemId);
    }
}
