using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Depreciations.Queries.GetAll;
public class GetAllDepreciationsQueryHandler : IRequestHandler<GetAllDepreciationsQuery, IEnumerable<Depreciation>>
{
    private readonly IDepreciationRepository _depreciationRepository;
    public GetAllDepreciationsQueryHandler(IDepreciationRepository depreciationRepository)
    {
        _depreciationRepository = depreciationRepository;
    }

    public async Task<IEnumerable<Depreciation>> Handle(GetAllDepreciationsQuery request, CancellationToken cancellationToken)
    {
        return await _depreciationRepository.GetAllAsync();
    }
}
