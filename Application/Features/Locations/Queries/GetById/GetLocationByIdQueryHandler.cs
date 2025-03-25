using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Locations.Queries.GetById;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Location?>
{
    private readonly ILocationRepository _locationRepository;

    public GetLocationByIdQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _locationRepository.GetByIdAsync(request.Id);
    }
}
