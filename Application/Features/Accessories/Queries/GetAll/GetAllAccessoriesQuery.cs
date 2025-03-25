using Domain.Entities;
using MediatR;

namespace Application.Features.Accessories.Queries.GetAll;

public record GetAllAccessoriesQuery : IRequest<IEnumerable<Accessory>>;
