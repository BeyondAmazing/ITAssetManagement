using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.StatusLabels.Queries.GetAll;
public class GetAllStatusLabelsQueryHandler : IRequestHandler<GetAllStatusLabelsQuery, IEnumerable<StatusLabel>>
{
    private readonly IStatusLabelRepository _statusLabelRepository;

    public GetAllStatusLabelsQueryHandler(IStatusLabelRepository statusLabelRepository)
    {
        _statusLabelRepository = statusLabelRepository;
    }

    public async Task<IEnumerable<StatusLabel>> Handle(GetAllStatusLabelsQuery request, CancellationToken cancellationToken)
    {
        return await _statusLabelRepository.GetAllAsync();
    }
}
