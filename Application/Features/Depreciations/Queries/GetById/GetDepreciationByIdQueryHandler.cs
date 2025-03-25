using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Depreciations.Queries.GetById;
public class GetDepreciationByIdQueryHandler : IRequestHandler<GetDepreciationByIdQuery, Depreciation?>
{
    private readonly IDepreciationRepository _depreciationRepository;

    public GetDepreciationByIdQueryHandler(IDepreciationRepository depreciationRepository)
    {
        _depreciationRepository = depreciationRepository;
    }

    public async Task<Depreciation?> Handle(GetDepreciationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _depreciationRepository.GetByIdAsync(request.Id);
    }
}
