using Domain.Entities;
using MediatR;

namespace Application.Features.Manufacturers.Queries.GetAll;

public record GetAllManufacturersQuery : IRequest<IEnumerable<Manufacturer>> { }
