using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Locations.Queries.GetAll;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllLocationsQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        return await _locationRepository.GetAllAsync();
    }
}
