using Domain.Entities;
using MediatR;

namespace Application.Features.Locations.Queries.GetById;

public record GetLocationByIdQuery(Guid Id) : IRequest<Location?>;
