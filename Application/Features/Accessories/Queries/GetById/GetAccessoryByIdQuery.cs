using Domain.Entities;
using MediatR;

namespace Application.Features.Accessories.Queries.GetById;

public record GetAccessoryByIdQuery(Guid Id) : IRequest<Accessory?> { }
