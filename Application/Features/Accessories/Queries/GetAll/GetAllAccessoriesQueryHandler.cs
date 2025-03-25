using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Accessories.Queries.GetAll;

public class GetAllAccessoriesQueryHandler : IRequestHandler<GetAllAccessoriesQuery, IEnumerable<Accessory>>
{
    private readonly IAccessoryRepository _accessoryRepository;

    public GetAllAccessoriesQueryHandler(IAccessoryRepository accessoryRepository)
    {
        _accessoryRepository = accessoryRepository;
    }

    public async Task<IEnumerable<Accessory>> Handle(GetAllAccessoriesQuery request, CancellationToken cancellationToken)
    {
        return await _accessoryRepository.GetAllAsync();
    }
}
