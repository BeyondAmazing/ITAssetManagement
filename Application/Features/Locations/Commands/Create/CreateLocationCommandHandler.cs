using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Locations.Commands.Create;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Location>
{
    private readonly ILocationRepository _locationRepository;

    public CreateLocationCommandHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Location> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = Location.Create(request.name, request.address, request.city, request.state, request.country, request.parentId);
        return await _locationRepository.AddAsync(location);
    }
}
