using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Components.Queries.GetAll;
public class GetAllComponentsQueryHandler : IRequestHandler<GetAllComponentsQuery, IEnumerable<Component>>
{
    private readonly IComponentRepository _componentRepository;

    public GetAllComponentsQueryHandler(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    public async Task<IEnumerable<Component>> Handle(GetAllComponentsQuery request, CancellationToken cancellationToken)
    {
        return await _componentRepository.GetAllAsync();
    }
}
