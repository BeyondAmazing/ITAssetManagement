using Domain.Entities;
using MediatR;

namespace Application.Features.Locations.Queries.GetAll;

public record GetAllLocationsQuery : IRequest<IEnumerable<Location>>;
