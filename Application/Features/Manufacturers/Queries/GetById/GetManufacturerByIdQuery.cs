using Domain.Entities;
using MediatR;

namespace Application.Features.Manufacturers.Queries.GetById;

public record GetManufacturerByIdQuery(Guid Id) : IRequest<Manufacturer?> { }
