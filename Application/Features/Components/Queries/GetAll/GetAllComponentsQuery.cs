using Domain.Entities;
using MediatR;

namespace Application.Features.Components.Queries.GetAll;

public record GetAllComponentsQuery : IRequest<IEnumerable<Component>> { }
