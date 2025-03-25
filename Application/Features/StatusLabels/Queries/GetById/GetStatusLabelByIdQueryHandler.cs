using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.StatusLabels.Queries.GetById;
public class GetStatusLabelByIdQueryHandler : IRequestHandler<GetStatusLabelByIdQuery, StatusLabel?>
{
    private readonly IStatusLabelRepository _statusLabelRepository;

    public GetStatusLabelByIdQueryHandler(IStatusLabelRepository statusLabelRepository)
    {
        _statusLabelRepository = statusLabelRepository;
    }

    public async Task<StatusLabel?> Handle(GetStatusLabelByIdQuery request, CancellationToken cancellationToken)
    {
        return await _statusLabelRepository.GetByIdAsync(request.Id);
    }
}
